using System.ComponentModel.Composition;
using Perm.Admin.AuditTrail.Data.Repository;
using Perm.Admin.AuditTrail.Module.Service;
using Perm.Core.DependencyResolver;
using Perm.Core.RequestManager.Processor;

namespace Perm.Admin.AuditTrail.Module
{
    [Export(typeof(IDependencyResolver))]
    public class AuditTrailDependencyResolver : IDependencyResolver
    {
        public void SetUp(IDependencyRegister dependencyRegister)
        {
            dependencyRegister.AddScoped<ServiceBase, GetAuditTrailService>();
            dependencyRegister.AddScoped<IAuditTrailRepository, AuditTrailRepository>();
        }
    }
}