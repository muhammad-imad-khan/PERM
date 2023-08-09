using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Perm.Core.TenantManager.Abstraction;
using Perm.Common;
using Perm.Model.Config;
using Perm.Model.Abstraction;
using Perm.Core.ExceptionManager;
using Perm.Core.RequestManager.Processor.PermException;
using Perm.Model.Setup;
using Perm.DataAccessLayer.Database.SqlServer.Interceptor;
using Perm.DataAccessLayer.Database.SqlServer.Loader;

namespace Perm.DataAccessLayer.Database.SqlServer;

public class PermDataContext : DbContext
{
    public static string RECORD_ALREADY_DELETED = "This record not exist.";
    public static string UNABLE_TO_DELETE = "8003";

    private readonly DbContextOptions<PermDataContext> _options;
    private readonly ITenantIdentificationService _tenantIdentificationService;

    public PermDataContext() { }

    public PermDataContext(ITenantIdentificationService tenantIdentificationServiceBase)
    {
        _tenantIdentificationService = tenantIdentificationServiceBase;
    }

    public PermDataContext(
        DbContextOptions<PermDataContext> options,
        ITenantIdentificationService tenantIdentificationServiceBase) : base(options)
    {
        _options = options;
        _tenantIdentificationService = tenantIdentificationServiceBase;
    }

    #region Interceptors

    private SessionContextInterceptor _sessionContextInterceptor;

    private SessionContextInterceptor GetSessionInterceptor()
    {
        if (_sessionContextInterceptor is null)
        {
            IEnumerable<IInterceptor> interceptors = (_options.Extensions.ToList()
                        .FirstOrDefault(
                            e => e is Microsoft.EntityFrameworkCore.Infrastructure.CoreOptionsExtension)
                    as Microsoft.EntityFrameworkCore.Infrastructure.CoreOptionsExtension)?
                .Interceptors;

            if (interceptors != null)
            {
                _sessionContextInterceptor = interceptors.FirstOrDefault(c => c is SessionContextInterceptor) as SessionContextInterceptor;
            }
        }
        return _sessionContextInterceptor;
    }

    #endregion

    #region Protected Methods

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        CurrentTenant ??= _tenantIdentificationService.GetCurrentTenant();

        ITenantDataBaseConfigModel databaseConfig = CurrentTenant.TenantDataBaseConfig;
        optionsBuilder.UseSqlServer(
            $"Data Source={databaseConfig.ServerName};" +
            $"Initial Catalog={databaseConfig.DatabaseName};" +
            $"User ID={databaseConfig.UserName};" +
            $"Password={databaseConfig.Password};" +
            $"trustServerCertificate=true");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        List<IMutableEntityType> entityTypes = modelBuilder.Model.GetEntityTypes().ToList();

        foreach (var foreignKey in entityTypes.SelectMany(e => e.GetForeignKeys()))
            foreignKey.DeleteBehavior = DeleteBehavior.NoAction;

        string[] seqNoFor = new[] { "ApplicationParamDetail", "ApplicationParamMaster", "CustomField", "CustomFieldMeta" };

        foreach (IMutableEntityType entityType in entityTypes.Where(t => t.ClrType.IsSubclassOf(typeof(ModelBase))))
        {
            string seqName = $"Seq_{entityType.GetTableName()}";
            modelBuilder.Entity(entityType.Name,
                x =>
                {
                    if (!seqNoFor.Contains(entityType.GetTableName()))
                        x.Property($"{entityType.GetTableName()}ID").UseHiLo(seqName, schema: entityType.GetSchema());
                });
        }

        modelBuilder.LoadEntities();

        foreach (IMutableProperty property in entityTypes.SelectMany(t => t.GetProperties()).Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetPrecision(18);
            property.SetScale(5);
        }
    }

    #endregion

    #region Properties

    public DbSet<ResponseMessageModel> ResponseMessage { get; set; }

    public DbSet<MenuModel> Menu { get; set; }
    public DbSet<PageOptionModel> PageOption { get; set; }
    public DbSet<APIInfoModel> APIInfo { get; set; }

    public ITenantConfigModel CurrentTenant { get; set; }

    #endregion

    #region Public Methods

    public static PermDataContext GetDataContext(IServiceProvider service)
    {
        using IServiceScope scopedDbContext = service.CreateScope();
        IServiceProvider serviceProvider = scopedDbContext.ServiceProvider;

        PermDataContext cubeDataContext = serviceProvider.GetRequiredService<PermDataContext>();
        IHttpContextAccessor httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
        ITenantIdentificationService tenantIdentificationService = serviceProvider.GetRequiredService<ITenantIdentificationService>();

        string tenantID = httpContextAccessor.HttpContext.Items["TenantID"].ToString();
        cubeDataContext.CurrentTenant = tenantIdentificationService.GetCurrentTenantByID(tenantID);
        return cubeDataContext;
    }


    public async Task CommitChangesAsync()
    {
        await using IDbContextTransaction transaction = await Database.BeginTransactionAsync();
        try
        {
            await SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            await transaction.RollbackAsync();
            throw new PermDatabaseException(RECORD_ALREADY_DELETED, "8002", ex);
        }
        catch (DbUpdateException ex)
        {
            string message = ex.Message;
            if (ex.InnerException is SqlException sqlException)
            {
                if (sqlException.Number == 547) //547 means, delete constraint exception
                {
                    if (sqlException.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                    {
                        string refereedTable = Regex.Match(sqlException.Message, @"table ""(.*)"", column").Groups[1]
                            .Value.Split('.')[1];
                        throw new PermDatabaseException(8003, refereedTable);
                    }
                }
                else if (sqlException.Number == 2601)//547 means, unique constraint exception
                {
                    Match valueMatch = Regex.Match(sqlException.Message, @"\((.*?)\)");
                    MatchCollection indexMatch = Regex.Matches(sqlException.Message, @"\'(.*?)\'");

                    string indexName = indexMatch[1].Groups[1].ToString();
                    string value = valueMatch.Groups[1].ToString();
                    throw new PermDuplicateRecordException(sqlException, indexName, value);
                }
            }
            await transaction.RollbackAsync();
            throw new PermDatabaseException(message, "8004", ex);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
        finally
        {
            ChangeTracker.Clear();
        }
    }

    public List<T> StoredProcedure<T>(string procName, object parameters) where T : new()
    {
        List<T> returnObj = new List<T>();

        Type type = typeof(T);
        IEnumerable<SqlParameter> sqlParameters = GetParametersFromObject(parameters);

        PropertyInfo[] propertyInfos = type.GetProperties();
        DbConnection dbConnection = Database.GetDbConnection();
        using DbCommand command = dbConnection.CreateCommand();
        command.CommandText = procName;
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddRange(sqlParameters.ToArray());

        bool wasOpen = dbConnection.State == ConnectionState.Open;
        try
        {
            if (!wasOpen)
                dbConnection.Open();

            GetSessionInterceptor().SetDbSession(this).Wait();

            using DbDataReader result = command.ExecuteReader();
            ReadOnlyCollection<DbColumn> columnSchema = result.GetColumnSchema();
            while (result.Read())
            {
                T df = new();

                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    if (columnSchema.Any(c => c.ColumnName == propertyInfo.Name))
                        propertyInfo.SetValue(df, (result[propertyInfo.Name] == DBNull.Value) ? null : result[propertyInfo.Name]);

                }

                returnObj.Add(df);
            }
        }
        finally
        {
            if (!wasOpen)
                dbConnection.Close();
        }

        return returnObj;
    }

    /// <summary>
    /// Execute scalar function on database with given parameters
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="functionName"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public T ScalarFunction<T>(string functionName, object parameters) where T : IConvertible
    {
        List<SqlParameter> sqlParameters = GetParametersFromObject(parameters).ToList();

        DbConnection dbConnection = Database.GetDbConnection();
        using DbCommand command = dbConnection.CreateCommand();
        command.CommandText = $" SELECT {functionName}({(sqlParameters.Count == 0 ? "" : string.Join(',', sqlParameters.Select(s => s.ParameterName)))})";
        command.CommandType = CommandType.Text;
        command.Parameters.AddRange(sqlParameters.ToArray());

        bool wasOpen = dbConnection.State == ConnectionState.Open;
        try
        {
            if (!wasOpen)
                dbConnection.Open();

            T result = command.ExecuteScalar().ParseTo<T>();
            return result;
        }
        finally
        {
            if (!wasOpen)
                dbConnection.Close();
        }
    }

    /// <summary>
    /// Execute SQL query on database with given parameters
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public List<T> SqlQuery<T>(string query) where T : new()
    {
        List<T> returnObj = new();
        DbConnection dbConnection = Database.GetDbConnection();
        using DbCommand command = dbConnection.CreateCommand();
        command.CommandText = query;
        command.CommandType = CommandType.Text;

        bool wasOpen = dbConnection.State == ConnectionState.Open;
        try
        {
            if (!wasOpen)
                dbConnection.Open();

            using DbDataReader result = command.ExecuteReader();
            ReadOnlyCollection<DbColumn> columnSchema = result.GetColumnSchema();

            Type type = typeof(T);
            PropertyInfo[] propertyInfos = type.GetProperties();
            while (result.Read())
            {
                T df = new();

                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    if (columnSchema.Any(c => c.ColumnName == propertyInfo.Name))
                        propertyInfo.SetValue(df, result[propertyInfo.Name]);

                }

                returnObj.Add(df);
            }

        }
        finally
        {
            if (!wasOpen)
                dbConnection.Close();
        }

        return returnObj;
    }

    public DataTable SqlQuery(string query)
    {
        DataTable dataTable = new();
        DbConnection dbConnection = Database.GetDbConnection();
        using DbCommand command = dbConnection.CreateCommand();
        command.CommandText = query;
        command.CommandType = CommandType.Text;

        bool wasOpen = dbConnection.State == ConnectionState.Open;
        try
        {
            if (!wasOpen)
                dbConnection.Open();

            using DbDataReader result = command.ExecuteReader();


            dataTable.Load(result);

        }
        finally
        {
            if (!wasOpen)
                dbConnection.Close();
        }

        return dataTable;
    }

    public DataSet SqlQuery(string query, bool isMultipleStatements)
    {
        DataSet dataSet = new();
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, Database.GetConnectionString());
        sqlDataAdapter.Fill(dataSet);

        return dataSet;
    }

    #endregion

    private static IEnumerable<SqlParameter> GetParametersFromObject(object parameters) =>
                (from propertyInfo in parameters.GetType().GetProperties()
                 let value = propertyInfo.GetValue(parameters)
                 where value != null
                 select new SqlParameter($"@{propertyInfo.Name}", value));
}