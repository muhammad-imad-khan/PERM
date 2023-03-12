using Perm.Config.ApplicationParamDetail.Component.Service;
using Perm.Config.ApplicationParamDetail.Data.Repository;
using Perm.Core.DependencyResolver;
using Perm.Core.RequestManager.Processor;
using System.ComponentModel.Composition;

namespace Perm.Config.ApplicationParamDetail.Component
{
    [Export(typeof(IDependencyResolver))]
    public class ApplicationParamDetailDependencyLoader : IDependencyResolver
    {
        public void SetUp(IDependencyRegister dependencyRegister)
        {
            dependencyRegister.AddScoped<ServiceBase, GetApplicationParamDetailService>();
            dependencyRegister.AddScoped<IApplicationParamDetailRepository, ApplicationParamDetailRepository>();
        }
    }
}