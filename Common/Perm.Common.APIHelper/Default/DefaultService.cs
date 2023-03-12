using System.Reflection;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using Perm.Common.APIModel;
using Perm.Core.DependencyResolver;
using Perm.Core.RequestManager.Processor;
using Perm.Core.TenantManager.Abstraction;
using Perm.DataAccessLayer.Database.SqlServer;

namespace Perm.Common.APIHelper.Default
{
    [StaticContent]
    public class DefaultService : ServiceBase
    {
        private readonly PermDataContext _dataContext;
        private readonly IOptions<List<TenantConfigModel>> _tenantConfigs;
        public override string ContentType => "text/html";
        public DefaultService(ILogger<ServiceBase> logger, PermDataContext dataContext, IOptions<List<TenantConfigModel>> tenantConfigs)
        {
            _dataContext = dataContext;
            _tenantConfigs = tenantConfigs;
        }

        public override string URL => "/";

        protected override Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            StringBuilder html = new StringBuilder(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "Default.html"));
            StringBuilder tableBody = new StringBuilder();

            var serviceBases = DependencyLoader.RegisteredServices.Where(c => c.URL != "/").ToList();
            for (var index = 0; index < serviceBases.Count; index++)
            {
                ServiceBase serviceBase = serviceBases[index];
                Assembly assembly = serviceBase.GetType().Assembly;
                tableBody.AppendLine("<tr>" +
                                     $"<td>{index + 1}</td>" +
                                     $"<td>{serviceBase.URL}</td>" +
                                     $"<td>{assembly.ManifestModule.ScopeName}</td>" +
                                     $"<td>{assembly.GetName().Version?.ToString()}</td>" +
                                     "</tr>");
            }

            html.Replace("@tableBody", tableBody.ToString());


            _dataContext.CurrentTenant = _tenantConfigs.Value.FirstOrDefault();
            List<IEntityType> entityTypes = _dataContext.Model.GetEntityTypes().ToList();
            StringBuilder tableMappingBody = new StringBuilder();
            for (var index = 0; index < entityTypes.Count; index++)
            {
                var entityType = entityTypes[index];
                tableMappingBody.AppendLine("<tr>" +
                                            $"<td>{index + 1}</td>" +
                                            $"<td>{entityType.GetSchemaQualifiedTableName()}</td>" +
                                            $"<td>{entityType.ClrType.Namespace}</td>" +
                                            $"<td>{entityType.ClrType.Name}</td>" +
                                            "</tr>");
            }
            html.Replace("@entityTableBody", tableMappingBody.ToString());

            return Task.FromResult(new ResponseModel<T> { Message = html.ToString() });
        }
    }
}
