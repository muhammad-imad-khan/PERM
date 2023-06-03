using Perm.Attendence.AttendanceRequest.Data.Repository;
using Perm.Attendence.AttendanceRequest.Module.Service;
using Perm.Core.DependencyResolver;
using Perm.Core.RequestManager.Processor;
using System.ComponentModel.Composition;

namespace Perm.Attendence.AttendanceRequest.Module
{
    [Export(typeof(IDependencyResolver))]
    public class AttendanceRequestDependencyResolver : IDependencyResolver
    {
        public void SetUp(IDependencyRegister dependencyRegister)
        {
            dependencyRegister.AddScoped<ServiceBase, AddAttendanceRequestService>();
            dependencyRegister.AddScoped<ServiceBase, UpdateAttendanceRequestService>();
            dependencyRegister.AddScoped<ServiceBase, DeleteAttendanceRequestService>();
            dependencyRegister.AddScoped<ServiceBase, GetAttendanceRequestService>();

            dependencyRegister.AddScoped<IAttendanceRequestRepository, AttendanceRequestRepository>();



        }
    }
}