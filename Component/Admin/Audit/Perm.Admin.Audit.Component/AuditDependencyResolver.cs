using System.ComponentModel.Composition;
using Perm.Admin.Audit.Data.Repository;
using Perm.Admin.Audit.Component.Service;
using Perm.Core.DependencyResolver;
using Perm.Core.RequestManager.Processor;

namespace Perm.Admin.Audit.Component
{
    [Export(typeof(IDependencyResolver))]
    public class AuditDependencyResolver : IDependencyResolver
    {
        public void SetUp(IDependencyRegister dependencyRegister)
        {
            dependencyRegister.AddScoped<ServiceBase, GetAuditService>();
            dependencyRegister.AddScoped<IAuditRepository, AuditRepository>();
        }
    }
}