using Perm.Core.DependencyResolver;
using Perm.Core.RequestManager.Processor;
using Perm.KPI.Report.Component.Service;
using Perm.KPI.Report.Data.Repository;
using System.ComponentModel.Composition;

namespace Perm.KPI.Report.Component
{
    [Export(typeof(IDependencyResolver))]

    public class ReportDependencyResolver : IDependencyResolver
    {
        public void SetUp(IDependencyRegister dependencyRegister)
        {
            dependencyRegister.AddScoped<ServiceBase, AttendanceChartService>();
            dependencyRegister.AddScoped<ServiceBase, EmployeeCountService>();
            dependencyRegister.AddScoped<ServiceBase, EmployeePerformanceService>();
            dependencyRegister.AddScoped<ServiceBase, LineChartService>();
            dependencyRegister.AddScoped<ServiceBase, TaskCountService>();

            dependencyRegister.AddScoped<IViewEmployeePerformanceRepository, ViewEmployeePerformanceRepository>();
        }
    }
}