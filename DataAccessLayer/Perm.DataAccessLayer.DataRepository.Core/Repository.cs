using EFCoreSecondLevelCacheInterceptor;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Primitives;
using Perm.Common;
using Perm.Core.TenantManager.Abstraction;
using Perm.DataAccessLayer.Database.SqlServer;
using Perm.DataAccessLayer.DataRepository.Core.Abstraction;
using Perm.DataAccessLayer.DataRepository.Core.Model;
using Perm.Model.Abstraction;
using System.Linq.Expressions;
using System.Reflection;

namespace Perm.DataAccessLayer.DataRepository.Core;

/// <inheritdoc />
public class Repository<T> : IRepository<T> where T : ModelBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    protected readonly PermDataContext Context;

    private DbSet<T> _entities;

    protected Repository(PermDataContext dataContext, IHttpContextAccessor httpContextAccessor)
    {
        Context = dataContext;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <inheritdoc />
    public ITenantConfigModel TenantConfigModel { get; set; }

    #region Query Methods

    /// <summary>
    /// Override this method to filter your entities
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    protected virtual IQueryable<T> Filter(IQueryable<T> entities)
    {
        return entities;
    }

    /// <inheritdoc />
    public PaginationQueryable<T> GetAllWithPagination(bool applyFilter = false)
    {
        (int pageNo, int rowCount, string sortBy, bool isAsc) = GetPaginationCriteriaFromHeader();

        GetSearchCriteriaFromHeader(out var searchColumn, out var searchValue, out long? customViewID, out long? entityTypeID);

        IQueryable<T> entities = GetAll(applyFilter);

        PaginationQueryable<T> paginationQueryable = entities.ApplyPaginationAndSorting(pageNo, rowCount, sortBy, isAsc);

        return paginationQueryable;
    }

    /// <inheritdoc />
    public PaginationQueryable<T> QueryWithPagination(Expression<Func<T, bool>> predict, bool applyFilter = false)
    {
        SetEntity(Context);
        (int pageNo, int rowCount, string sortBy, bool isAsc) = GetPaginationCriteriaFromHeader();

        IQueryable<T> query = Query(predict, applyFilter);

        return query.ApplyPaginationAndSorting(pageNo, rowCount, sortBy, isAsc);
    }

    /// <inheritdoc />
    public IQueryable<T> GetAll(bool applyFilter = false)
    {
        GetSearchCriteriaFromHeader(out var searchColumn, out var searchValue, out long? customViewID,
            out long? entityTypeID);

        SetEntity(Context);
        IQueryable<T> entities = _entities;

        entities = IncludeForeignKeys(_entities) ?? entities;

        entities = Filter(entities);

        if (applyFilter)
        {
            if (!string.IsNullOrEmpty(searchColumn) && !string.IsNullOrEmpty(searchValue) &&
                searchValue != "undefined")
            {
                entities = entities.Where(BuildFilterExpression<T>(searchColumn, searchValue));
            }
        }

        return entities;
    }

    /// <inheritdoc />
    public IQueryable<T> Query(Expression<Func<T, bool>> predict, bool applyFilter = false) => GetAll(applyFilter).Where(predict);

    /// <inheritdoc />
    public IQueryable<T> Query(FormattableString query)
    {
        SetEntity(Context);
        return _entities.FromSql<T>(query);
    }

    /// <inheritdoc />
    public async Task<T> FirstOrNullAsync(Expression<Func<T, bool>> predict)
    {
        SetEntity(Context);

        IQueryable<T> modelBases = _entities.Cacheable(CacheExpirationMode.Absolute, TimeSpan.FromMinutes(45));

        modelBases = modelBases.AsNoTracking();

        return await modelBases.FirstOrDefaultAsync(predict);
    }

    /// <summary>
    /// If you want to include foreign key in your entity, then you can override this method in your repository and include your foreign keys here.
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    protected virtual IIncludableQueryable<T, object> IncludeForeignKeys(IQueryable<T> entities)
    {
        return null;
    }

    #endregion

    #region Command Methods

    /// <inheritdoc />
    public async Task AddAsync(T entity, Func<T, long> entityID = null)
    {
        SetEntity(Context);

        #region "Set Audit Columns"

        entity.IsDeleted = false;
        entity.CreatedOn = DateTime.Now;
        object httpContextItem = _httpContextAccessor.HttpContext?.Items["UserID"];
        if (httpContextItem != null && httpContextItem.ParseTo<long>() != 0)
            entity.CreatedBy = httpContextItem.ParseTo<long>();

        #endregion

        await _entities.AddAsync(entity);
    }

    /// <inheritdoc />
    public async Task AddRange(List<T> entity)
    {
        foreach (T modelBase in entity)
        {
            await AddAsync(modelBase);
        }
    }

    /// <inheritdoc />
    public void Update(T entity, long entityID = 0)
    {
        Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        #region RESTRICT AUDIT COLUMNS FROM UPDATING

        EntityEntry<T> entityEntry = Context.Entry(entity);
        entityEntry.State = EntityState.Modified;
        string[] excluded = { "CreatedBy", "CreatedOn", "IsDeleted" };

        foreach (string exc in excluded)
            entityEntry.Property(exc).IsModified = false;

        #endregion
    }

    /// <inheritdoc />
    public async Task Update(List<T> entityList, Func<T, long> primaryKey)
    {
        foreach (T entity in entityList.Where(m => primaryKey(m) > 0 && !m.IsDeleted))
            Update(entity);

        DeleteRange(entityList.Where(m => m.IsDeleted && primaryKey(m) > 0).ToList());

        await AddRange(entityList.Where(m => primaryKey(m) <= 0 && !m.IsDeleted).ToList());
    }

    /// <inheritdoc />
    public void Delete(T entity, long entityID = 0, bool setIsDeletedFlag = false)
    {
        Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        if (CheckIfSetDeleteFlag(out bool isDelete))
        {
            EntityEntry<T> entityEntry = Context.Entry(entity);
            entityEntry.State = EntityState.Modified;
            entity.IsDeleted = isDelete;

            foreach (PropertyEntry entityEntryProperty in entityEntry.Properties)
            {
                entityEntryProperty.IsModified = entityEntryProperty.Metadata.Name == "IsDeleted";
            }
        }
        else if (setIsDeletedFlag)
        {
            EntityEntry<T> entityEntry = Context.Entry(entity);
            entityEntry.State = EntityState.Modified;
            entity.IsDeleted = setIsDeletedFlag;

            foreach (PropertyEntry entityEntryProperty in entityEntry.Properties)
            {
                entityEntryProperty.IsModified = entityEntryProperty.Metadata.Name == "IsDeleted";
            }
        }
        else
        {
            SetEntity(Context);
            _entities.Remove(entity);
        }
    }

    /// <inheritdoc />
    public void DeleteRange(IEnumerable<T> entity)
    {
        if (CheckIfSetDeleteFlag(out bool isDelete))
        {
            foreach (T modelBase in entity)
            {
                EntityEntry<T> entityEntry = Context.Entry(modelBase);
                entityEntry.State = EntityState.Modified;
                modelBase.IsDeleted = isDelete;

                foreach (PropertyEntry entityEntryProperty in entityEntry.Properties)
                    entityEntryProperty.IsModified = entityEntryProperty.Metadata.Name == "IsDeleted";
            }
        }
        else
        {
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            SetEntity(Context);
            _entities.RemoveRange(entity);
        }
    }

    #endregion

    #region Private Methods

    private static Expression<Func<TU, bool>> BuildFilterExpression<TU>(string propertyName, object value)
    {
        ParameterExpression parameter = Expression.Parameter(typeof(TU));
        MemberExpression left = Expression.Property(parameter, propertyName);

        if (left.Type == typeof(string))
        {
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            ConstantExpression someValue = Expression.Constant(value, typeof(string));
            MethodCallExpression containsMethodExp = Expression.Call(left, method!, someValue);
            return Expression.Lambda<Func<TU, bool>>(containsMethodExp, parameter);
        }

        if (left.Type == typeof(long) && value?.ToString()?.Split(',').Length > 1)
        {
            List<long> longArray = value?.ToString()?.Split(',').Select(c => c.ParseTo<long>()).ToList();
            MethodInfo containsMethodInfo = typeof(List<long>).GetMethod("Contains", new[] { typeof(long) });
            ConstantExpression someValue = Expression.Constant(longArray, typeof(List<long>));
            MethodCallExpression containsMethodExp = Expression.Call(someValue, containsMethodInfo!, left);
            return Expression.Lambda<Func<TU, bool>>(containsMethodExp, parameter);
        }

        Type leftType = Nullable.GetUnderlyingType(left.Type) ?? left.Type;
        Expression right = Expression.Constant(Convert.ChangeType(value, leftType));
        right = Expression.Convert(right, left.Type);

        Expression e1 = Expression.Equal(left, right);
        return Expression.Lambda<Func<TU, bool>>(e1, parameter);
    }

    /// <summary>
    ///     cannot use this method in constructor, because it is causing connection string to be change, and at this stage we
    ///     cannot change the connection string because tenant configuration is unknown.
    /// </summary>
    /// <param name="dataContext"></param>
    private void SetEntity(PermDataContext dataContext)
    {
        _entities ??= dataContext.Set<T>();
        TenantConfigModel = dataContext.CurrentTenant;
    }

    /// <summary>
    ///     If any search criteria exist, Get it from header
    /// </summary>
    /// <param name="searchColumn"></param>
    /// <param name="searchValue"></param>
    /// <param name="customViewID"></param>
    /// <param name="entityTypeID"></param>
    private void GetSearchCriteriaFromHeader(out string searchColumn, out string searchValue, out long? customViewID, out long? entityTypeID)
    {
        searchColumn = null;
        searchValue = null;
        customViewID = null;
        entityTypeID = null;

        if (_httpContextAccessor.HttpContext == null)
        {
            return;
        }

        IHeaderDictionary headers = _httpContextAccessor.HttpContext.Request.Headers;


        if (headers.TryGetValue("searchColumn", out StringValues hValue))
        {
            searchColumn = hValue[0];
        }

        if (headers.TryGetValue("searchValue", out hValue))
        {
            searchValue = hValue[0];
        }
        if (headers.TryGetValue("customViewID", out hValue))
        {

            customViewID = hValue[0].ParseTo<long>() == 0 ? null : hValue[0].ParseTo<long>();
        }
        if (headers.TryGetValue("entityTypeID", out hValue))
        {
            entityTypeID = hValue[0].ParseTo<long>() == 0 ? null : hValue[0].ParseTo<long>();
        }
    }

    /// <summary>
    /// Check if record is queering for one record or many records.
    /// </summary>
    /// <returns></returns>
    protected bool IsSearchByID()
    {
        GetSearchCriteriaFromHeader(out string searchColumn, out string searchValue, out _, out _);

        return !string.IsNullOrEmpty(searchValue) && !string.IsNullOrEmpty(searchColumn);
    }

    private bool CheckIfSetDeleteFlag(out bool isDelete)
    {
        IHeaderDictionary headers = _httpContextAccessor.HttpContext.Request.Headers;

        if (headers.TryGetValue("disable", out StringValues hValue))
        {
            isDelete = hValue.Count > 0 && hValue[0] == "1";
            return true;
        }

        isDelete = false;
        return false;
    }

    /// <inheritdoc />
    public (int pageNo, int rowCount, string sortBy, bool isAsc) GetPaginationCriteriaFromHeader()
    {
        int pageNo = 1;
        int rowCount = 0; //Constant.DEFAULT_PAGE_ROW_COUNT;
        string sortBy = null;
        bool isAsc = true;
        IHeaderDictionary headers = _httpContextAccessor.HttpContext.Request.Headers;

        if (headers.TryGetValue("pageNo", out StringValues hValue))
        {
            pageNo = hValue[0].ParseTo<int>();
        }

        if (headers.TryGetValue("rowCount", out hValue))
        {
            rowCount = hValue[0].ParseTo<int>();
        }

        if (headers.TryGetValue("sortBy", out hValue))
        {
            sortBy = hValue[0];
        }

        if (headers.TryGetValue("isAsc", out hValue))
        {
            isAsc = hValue[0].ParseTo<bool>();
        }

        return (pageNo, rowCount, sortBy, isAsc);
    }

    #endregion
}