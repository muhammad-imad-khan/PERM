using Microsoft.EntityFrameworkCore;
using Perm.DataAccessLayer.Database.SqlServer.Loader;
using Perm.Model.Admin;
using System.ComponentModel.Composition;

namespace Perm.Admin.Role.Data
{
    [Export(typeof(IEntityResolver))]
    public class EntityResolver : IEntityResolver
    {
        public void SetUp(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity(typeof(RoleModel)).Property("RoleID").UseHiLo("Seq_Role", "Admin");
        }
    }
}