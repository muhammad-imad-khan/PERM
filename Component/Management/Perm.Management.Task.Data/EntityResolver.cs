using Microsoft.EntityFrameworkCore;
using Perm.DataAccessLayer.Database.SqlServer.Loader;
using Perm.Model.Management;
using System.ComponentModel.Composition;

namespace Perm.Management.Tasks.Data
{
    [Export(typeof(IEntityResolver))]
    public class EntityResolver : IEntityResolver
    {
        public void SetUp(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity(typeof(TaskModel)).Property("TaskID").UseHiLo("Seq_Tasks", "Management");
        }
    }
}