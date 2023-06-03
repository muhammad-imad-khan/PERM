using Microsoft.EntityFrameworkCore;
using Perm.DataAccessLayer.Database.SqlServer.Loader;
using Perm.Model.Attendence;
using System.ComponentModel.Composition;
using System.Reflection.Emit;

namespace Perm.Attendence.AttendanceAssistant.Data
{
    [Export(typeof(IEntityResolver))]
    public class EntityResolver : IEntityResolver
    {
        public void SetUp(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity(typeof(AttendanceAssistantModel)).Property("AttendanceAssistantID").UseHiLo("Seq_Atd", "Attendence");
        }
    }
}