using Perm.Common.APIModel;
using Perm.Core.RequestManager.Processor;
using Perm.EmployeeMasterData.BusinessPartner.Component.APIModel;
using Perm.EmployeeMasterData.BusinessPartner.Data.Repository.Abstraction;

namespace Perm.EmployeeMasterData.BusinessPartner.Component.Service
{
    [Authenticate]
    public class DeleteBusinessPartnerService : ServiceBase
    {
        public override string URL => "/api/BusinessPartner/Delete";
        public override HttpMethod HttpMethod => HttpMethod.Delete;
        private readonly IBusinessPartnerRepository _businessPartnerRepository;
        public DeleteBusinessPartnerService(IBusinessPartnerRepository businessPartnerRepository)
        {
            _businessPartnerRepository = businessPartnerRepository;
        }

        protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            ReqDeleteBusinessPartnerModel reqModel = CastToObject<ReqDeleteBusinessPartnerModel>(requestModel);

            await _businessPartnerRepository.DeleteBusinessPartner(reqModel.BusinessPartnerID);

            return new ResponseModel<T>();
        }

        public Task<ResponseModel<T>> ProcessRole<T>(IRequestModel requestModel)
        {
            return ExecuteComponentAsync<T>(requestModel);
        }

    }
}
