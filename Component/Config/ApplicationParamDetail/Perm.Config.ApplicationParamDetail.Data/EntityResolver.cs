using Microsoft.EntityFrameworkCore;
using Perm.DataAccessLayer.Database.SqlServer.Loader;
using Perm.Model.Abstraction;
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