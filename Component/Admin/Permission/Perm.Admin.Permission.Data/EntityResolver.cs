using Microsoft.EntityFrameworkCore;
using Perm.DataAccessLayer.Database.SqlServer.Loader;
using Perm.Model.Admin;
using System.ComponentModel.Composition;

namespace Perm.Admin.Permission.Data
{
    [Export(typeof(IEntityResolver))]
    public class EntityResolver : IEntityResolver
    {
        public void SetUp(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity(typeof(PermissionModel)).Property("PermissionID").UseHiLo("Seq_Permission", "Admin");
            modelBuilder.Entity(typeof(PermissionDetailModel)).Property("PermissionDetailID").UseHiLo("Seq_PermissionDetail", "Admin");
        }
    }
}