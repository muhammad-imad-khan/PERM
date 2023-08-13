using Perm.Core.DependencyResolver;
using Perm.Core.RequestManager.Processor;
using Perm.Management.Tasks.Component.Service;
using Perm.Management.Tasks.Data.Repository;
using Perm.Management.Tasks.Module.Service;
using System.ComponentModel.Composition;

namespace Perm.Management.Tasks.Module
{
    [Export(typeof(IDependencyResolver))]
    public class TasksDependencyResolver : IDependencyResolver
    {
        public void SetUp(IDependencyRegister dependencyRegister)
        {
            dependencyRegister.AddScoped<ServiceBase, AddTaskService>();
            dependencyRegister.AddScoped<ServiceBase, UpdateTaskService>();
            dependencyRegister.AddScoped<ServiceBase, DeleteTaskService>();
            dependencyRegister.AddScoped<ServiceBase, GetTaskService>();
            dependencyRegister.AddScoped<ServiceBase, MarkAsDoneTaskService>();

            dependencyRegister.AddScoped<ITaskRepository, TaskRepository>();



        }
    }
}