using Perm.Attendence.MarkAttendance.Data.Repository;
using Perm.Attendence.MarkAttendanceModule.Service;
using Perm.Core.DependencyResolver;
using Perm.Core.RequestManager.Processor;
using System.ComponentModel.Composition;

namespace Perm.Attendence.MarkAttendanceModule
{
    [Export(typeof(IDependencyResolver))]
    public class MarkAttendanceDependencyResolver : IDependencyResolver
    {
        public void SetUp(IDependencyRegister dependencyRegister)
        {
            dependencyRegister.AddScoped<ServiceBase, AddMarkAttendanceService>();
            dependencyRegister.AddScoped<ServiceBase, UpdateMarkAttendanceService>();
            dependencyRegister.AddScoped<ServiceBase, DeleteMarkAttendanceService>();
            dependencyRegister.AddScoped<ServiceBase, GetMarkAttendanceService>();

            dependencyRegister.AddScoped<IMarkAttendanceRepository, MarkAttendanceRepository>();



        }
    }
}