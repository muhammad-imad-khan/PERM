using Perm.Admin.Permission.Component.Service;
using Perm.Admin.Permission.Data.Repository;
using Perm.Admin.Permission.Data.Repository.Abstraction;
using Perm.Core.DependencyResolver;
using Perm.Core.RequestManager.Processor;
using System.ComponentModel.Composition;

namespace Perm.Admin.Permission.Component
{
    [Export(typeof(IDependencyResolver))]
    public class UserDependencyResolver : IDependencyResolver
    {
        public void SetUp(IDependencyRegister dependencyRegister)
        {
            dependencyRegister.AddScoped<ServiceBase, AddPermissionService>();
            dependencyRegister.AddScoped<ServiceBase, UpdatePermissionService>();
            dependencyRegister.AddScoped<ServiceBase, DeletePermissionService>();
            dependencyRegister.AddScoped<ServiceBase, GetPermissionService>();

            dependencyRegister.AddScoped<IPermissionRepository, PermissionRepository>();
            dependencyRegister.AddScoped<IPermissionDetailRepository, PermissionDetailRepository>();
        }
    }
}