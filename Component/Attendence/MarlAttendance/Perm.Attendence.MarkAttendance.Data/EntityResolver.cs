using Microsoft.EntityFrameworkCore;
using Perm.DataAccessLayer.Database.SqlServer.Loader;
using Perm.Model.Attendance;
using System.ComponentModel.Composition;

namespace Perm.Attendence.MarkAttendance.Data
{
    [Export(typeof(IEntityResolver))]
    public class EntityResolver : IEntityResolver
    {
        public void SetUp(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity(typeof(MarkAttendanceModel)).Property("MarkAttendanceID").UseHiLo("Seq_Atd","Attendence");
        }
    }
}