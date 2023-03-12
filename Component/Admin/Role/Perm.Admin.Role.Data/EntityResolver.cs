using Microsoft.EntityFrameworkCore;
using Perm.Admin.Model;
using Perm.DataAccessLayer.Database.SqlServer;
using System.ComponentModel.Composition;

namespace Perm.Admin.Role.Data
{
    [Export(typeof(IEntityResolver))]
    public class EntityResolver : IEntityResolver
    {
        public void SetUp(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity(typeof(RoleModel)).Property("UserID").UseHiLo("Seq_Role", "Admin");
        }
    }
}