using Microsoft.EntityFrameworkCore;
using Perm.DataAccessLayer.Database.SqlServer.Loader;
using Perm.Model.EmployeeMasterData;
using System.ComponentModel.Composition;

namespace Perm.EmployeeMasterData.BusinessPartner.Data
{
    [Export(typeof(IEntityResolver))]
    public class EntityResolver : IEntityResolver
    {
        public void SetUp(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity(typeof(BusinessPartnerModel)).Property("BusinessPartnerID").UseHiLo("Seq_BusinessPartner", "EmployeeMasterData");
        }
    }
}