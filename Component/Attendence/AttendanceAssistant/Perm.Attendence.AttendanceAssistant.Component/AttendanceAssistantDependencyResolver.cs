using Perm.Attendence.AttendanceAssistant.Data.Repository;
using Perm.Attendence.AttendanceAssistant.Module.Service;
using Perm.Core.DependencyResolver;
using Perm.Core.RequestManager.Processor;
using System.ComponentModel.Composition;

namespace Perm.Attendence.AttendanceAssistant.Module
{

    [Export(typeof(IDependencyResolver))]
    public class AttendanceAssistantDependencyResolver : IDependencyResolver
    {
        public void SetUp(IDependencyRegister dependencyRegister)
        {
            dependencyRegister.AddScoped<ServiceBase, AddAttendanceAssistantService>();
            dependencyRegister.AddScoped<ServiceBase, UpdateAttendanceAssistantService>();
            dependencyRegister.AddScoped<ServiceBase, DeleteAttendanceAssistantService>();
            dependencyRegister.AddScoped<ServiceBase, GetAttendanceAssistantService>();

            dependencyRegister.AddScoped<IAttendanceAssistantRepository, AttendanceAssistantRepository>();



        }
    }
}