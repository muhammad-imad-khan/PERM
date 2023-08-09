using Perm.Core.DependencyResolver;
using Perm.Core.RequestManager.Processor;
using Perm.Setup.Common.Component.Service;
using Perm.Setup.Common.Data.Repository;
using Perm.Setup.Common.Data.Repository.Abstraction;
using System.ComponentModel.Composition;

namespace Perm.Setup.Common.Component
{
    [Export(typeof(IDependencyResolver))]
    public class UserDependencyResolver : IDependencyResolver
    {
        public void SetUp(IDependencyRegister dependencyRegister)
        {
            dependencyRegister.AddScoped<ServiceBase, GetMenuService>();
            dependencyRegister.AddScoped<ServiceBase, GetPageOptionService>();

            dependencyRegister.AddScoped<IMenuRepository, MenuRepository>();
            dependencyRegister.AddScoped<IPageOptionRepository, PageOptionRepository>();
        }
    }
}