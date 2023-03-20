using System.ComponentModel.Composition;
using Microsoft.EntityFrameworkCore;
using Perm.DataAccessLayer.Database.SqlServer.Loader;

namespace Perm.Admin.Audit.Data;

[Export(typeof(IEntityResolver))]
public class EntityResolver : IEntityResolver
{
    public void SetUp(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity(typeof(ViewAuditModel));
    }
}