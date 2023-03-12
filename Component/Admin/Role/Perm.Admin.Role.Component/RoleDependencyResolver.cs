using Perm.Admin.Role.Component.Service;
using Perm.Admin.Role.Data.Repository;
using Perm.Core.DependencyResolver;
using Perm.Core.RequestManager.Processor;
using System.ComponentModel.Composition;

namespace Perm.Admin.Role.Component
{
    [Export(typeof(IDependencyResolver))]
    public class RoleDependencyResolver : IDependencyResolver
    {
        public void SetUp(IDependencyRegister dependencyRegister)
        {
            dependencyRegister.AddScoped<ServiceBase, AddRoleService>();
            dependencyRegister.AddScoped<ServiceBase, UpdateRoleService>();
            dependencyRegister.AddScoped<ServiceBase, DeleteRoleService>();
            dependencyRegister.AddScoped<ServiceBase, GetRoleService>();

            dependencyRegister.AddScoped<IRoleRepository, RoleRepository>();
        }
    }
}