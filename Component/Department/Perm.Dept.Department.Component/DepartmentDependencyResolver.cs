using Perm.Core.DependencyResolver;
using Perm.Core.RequestManager.Processor;
using Perm.Dept.Department.Data.Repository;
using Perm.Dept.Department.Module.Service;
using System.ComponentModel.Composition;

namespace Perm.Dept.Department.Module
{
    [Export(typeof(IDependencyResolver))]
    public class DepartmentDependencyResolver : IDependencyResolver
    {
        public void SetUp(IDependencyRegister dependencyRegister)
        {
            dependencyRegister.AddScoped<ServiceBase, AddDepartmentService>();
            dependencyRegister.AddScoped<ServiceBase, UpdateDepartmentService>();
            dependencyRegister.AddScoped<ServiceBase, DeleteDepartmentService>();
            dependencyRegister.AddScoped<ServiceBase, GetDepartmentService>();

            dependencyRegister.AddScoped<IDepartmentRepository, DepartmentRepository>();



        }
    }
}