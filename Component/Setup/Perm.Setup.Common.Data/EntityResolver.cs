using Microsoft.EntityFrameworkCore;
using Perm.DataAccessLayer.Database.SqlServer.Loader;
using Perm.Model.Setup;
using System.ComponentModel.Composition;

namespace Perm.Setup.Common.Data
{
    [Export(typeof(IEntityResolver))]
    public class EntityResolver : IEntityResolver
    {
        public void SetUp(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity(typeof(MenuModel));
            modelBuilder.Entity(typeof(PageOptionModel));
        }
    }
}