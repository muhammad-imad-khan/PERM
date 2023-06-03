using Perm.DataAccessLayer.DataRepository.Core.Abstraction;
using Perm.Model.EmployeeMasterData;

namespace Perm.EmployeeMasterData.BusinessPartner.Data.Repository.Abstraction
{
    public interface IBusinessPartnerRepository : IRepository<BusinessPartnerModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessPartnerModel"></param>
        /// <returns></returns>
        public Task AddBusinessPartner(BusinessPartnerModel businessPartnerModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessPartnerModel"></param>
        /// <returns></returns>
        public Task UpdateBusinessPartner(BusinessPartnerModel businessPartnerModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessPartnerID"></param>
        /// <returns></returns>
        public Task DeleteBusinessPartner(long businessPartnerID);
    }
}
