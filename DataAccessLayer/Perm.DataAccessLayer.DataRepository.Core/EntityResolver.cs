using Microsoft.EntityFrameworkCore;
using Perm.DataAccessLayer.Database.SqlServer.Loader;
using Perm.Model.Abstraction;
using System.ComponentModel.Composition;

namespace Perm.DataAccessLayer.DataRepository.Core
{
    [Export(typeof(IEntityResolver))]
    public class EntityResolver : IEntityResolver
    {
        public void SetUp(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity(typeof(CustomFieldModel)).Property("CustomFieldID").UseHiLo("Seq_CustomField", "Config");
            modelBuilder.Entity(typeof(CustomFieldMetaModel));
        }
    }
}