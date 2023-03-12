using Perm.Admin.Login.Component.Helper;
using Perm.Admin.Login.Component.Service;
using Perm.Core.DependencyResolver;
using Perm.Core.RequestManager.Processor;
using System.ComponentModel.Composition;

namespace Perm.Admin.Login.Component
{
    [Export(typeof(IDependencyResolver))]
    public class LoginDependencyResolver : IDependencyResolver
    {
        public void SetUp(IDependencyRegister dependencyRegister)
        {
            dependencyRegister.AddScoped<MenuGenerator>();
            dependencyRegister.AddScoped<ServiceBase, LoginService>();
        }
    }
}