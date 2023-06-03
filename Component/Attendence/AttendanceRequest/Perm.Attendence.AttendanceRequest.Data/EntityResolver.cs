using Microsoft.EntityFrameworkCore;
using Perm.DataAccessLayer.Database.SqlServer.Loader;
using Perm.Model.Attendence;
using System.ComponentModel.Composition;
using System.Reflection.Emit;

namespace Perm.Attendence.AttendanceRequest.Data
{
    [Export(typeof(IEntityResolver))]
    public class EntityResolver : IEntityResolver
    {
        public void SetUp(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity(typeof(AttendanceRequestModel)).Property("AttendanceRequestID").UseHiLo("Seq_AttendanceRequest", "Attendence");
        }
    }
}