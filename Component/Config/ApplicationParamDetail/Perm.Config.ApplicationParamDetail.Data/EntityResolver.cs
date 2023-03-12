using Microsoft.EntityFrameworkCore;
using Perm.DataAccessLayer.Database.SqlServer;
using Perm.Model.Config;
using System.ComponentModel.Composition;

namespace Perm.Config.ApplicationParamDetail.Data
{
    [Export(typeof(IEntityResolver))]
    public class EntityResolver : IEntityResolver
    {
        public void SetUp(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity(typeof(ApplicationParamDetailModel));
        }
    }
}