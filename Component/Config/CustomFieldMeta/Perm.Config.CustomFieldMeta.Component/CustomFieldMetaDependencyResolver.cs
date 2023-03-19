using Perm.Config.CustomFieldMeta.Component.Service;
using Perm.Core.DependencyResolver;
using Perm.Core.RequestManager.Processor;
using Perm.DataAccessLayer.DataRepository.Core.Abstraction;
using Perm.DataAccessLayer.DataRepository.Core.CustomField;
using System.ComponentModel.Composition;

namespace Perm.Config.CustomFieldMeta.Component
{
    [Export(typeof(IDependencyResolver))]
    public class CustomFieldMetaDependencyResolver : IDependencyResolver
    {
        public void SetUp(IDependencyRegister dependencyRegister)
        {
            dependencyRegister.AddScoped<ServiceBase, GetCustomFieldMetaService>();
            dependencyRegister.AddScoped<ServiceBase, AddCustomFieldMetaService>();
            dependencyRegister.AddScoped<ServiceBase, UpdateCustomFieldMetaService>();
            dependencyRegister.AddScoped<ServiceBase, DeleteCustomFieldMetaService>();

            dependencyRegister.AddScoped<ICustomFieldRepository, CustomFieldRepository>();
            dependencyRegister.AddScoped<ICustomFieldMetaRepository, CustomFieldMetaRepository>();
        }
    }
}