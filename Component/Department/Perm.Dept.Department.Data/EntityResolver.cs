using Microsoft.EntityFrameworkCore;
using Perm.DataAccessLayer.Database.SqlServer.Loader;
using Perm.Model.Department;
using System.ComponentModel.Composition;

namespace Perm.Dept.Department.Data
{
    [Export(typeof(IEntityResolver))]
    public class EntityResolver : IEntityResolver
    {
        public void SetUp(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity(typeof(DepartmentModel)).Property("DepartmentID").UseHiLo("Seq_Dept","Dept");
        }
    }
}