using Microsoft.EntityFrameworkCore;
using Perm.DataAccessLayer.Database.SqlServer.Loader;
using Perm.KPI.Report.Data.Model;
using System.ComponentModel.Composition;

namespace Perm.KPI.Report.Data
{
    [Export(typeof(IEntityResolver))]

    public class EntityResolver : IEntityResolver
    {
        public void SetUp(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity(typeof(View_EmployeePerformanceModel));
        }
    }
}