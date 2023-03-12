using Microsoft.EntityFrameworkCore;
using Perm.Admin.Model;
using Perm.DataAccessLayer.Database.SqlServer;
using System.ComponentModel.Composition;

namespace Perm.Admin.User.Data
{
    [Export(typeof(IEntityResolver))]
    public class EntityResolver : IEntityResolver
    {
        public void SetUp(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity(typeof(UserModel)).Property("UserID").UseHiLo("Seq_User", "Admin");
            modelBuilder.Entity(typeof(UserRoleModel)).Property("UserRoleID").UseHiLo("Seq_UserRole", "Admin");
        }
    }
}