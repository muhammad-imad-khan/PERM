using Perm.Admin.User.Component.Service;
using Perm.Admin.User.Data.Repository;
using Perm.Core.DependencyResolver;
using Perm.Core.RequestManager.Processor;
using System.ComponentModel.Composition;

namespace Perm.Admin.User.Component
{
    [Export(typeof(IDependencyResolver))]
    public class UserDependencyResolver : IDependencyResolver
    {
        public void SetUp(IDependencyRegister dependencyRegister)
        {
            dependencyRegister.AddScoped<ServiceBase, AddUserService>();
            dependencyRegister.AddScoped<ServiceBase, UpdateUserService>();
            dependencyRegister.AddScoped<ServiceBase, DeleteUserService>();
            dependencyRegister.AddScoped<ServiceBase, GetUserService>();
            dependencyRegister.AddScoped<ServiceBase, GetUserAndRoleService>();

            dependencyRegister.AddScoped<IUserRepository, UserRepository>();
            dependencyRegister.AddScoped<IUserRoleRepository, UserRoleRepository>();
        }
    }
}