using Perm.Common.APIModel;
using Perm.Core.RequestManager.Processor;
using Perm.DataAccessLayer.DataRepository.Core.Model;
using Perm.EmployeeMasterData.BusinessPartner.Data.Repository.Abstraction;
using Perm.Model.EmployeeMasterData;

namespace Perm.EmployeeMasterData.BusinessPartner.Component.Service
{
    [Authenticate]
    public class GetBusinessPartnerService : ServiceBase
    {
        public override string URL => "/api/BusinessPartner";
        public override HttpMethod HttpMethod => HttpMethod.Get;
        private readonly IBusinessPartnerRepository _businessPartnerRepository;

        public GetBusinessPartnerService(IBusinessPartnerRepository businessPartnerRepository)
        {
            _businessPartnerRepository = businessPartnerRepository;
        }

        protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            PaginationList<BusinessPartnerModel> entitiesList = await _businessPartnerRepository.GetAllWithPagination(applyFilter: true).ToPaginationListAsync();

            return new ResponseModel<T>
            {
                Data = (T)(object)entitiesList
            };
        }
    }
}
