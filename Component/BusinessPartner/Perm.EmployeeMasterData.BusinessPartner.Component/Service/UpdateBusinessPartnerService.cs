using Perm.Common.APIModel;
using Perm.Core.ExceptionManager;
using Perm.Core.RequestManager.Processor.PermException;
using Perm.Core.RequestManager.Processor;
using Perm.EmployeeMasterData.BusinessPartner.Data.Repository.Abstraction;
using Perm.EmployeeMasterData.BusinessPartner.Component.APIModel;

namespace Perm.EmployeeMasterData.BusinessPartner.Component.Service
{
    [Authenticate]
    public class UpdateBusinessPartnerService : ServiceBase
    {
        public override string URL => "/api/BusinessPartner/Update";
        public override HttpMethod HttpMethod => HttpMethod.Put;
        private readonly IBusinessPartnerRepository _businessPartnerRepository;

        public UpdateBusinessPartnerService(IBusinessPartnerRepository businessPartnerRepository)
        {
            _businessPartnerRepository = businessPartnerRepository;
        }

        protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            ReqAddBusinessPartnerModel reqModel = CastToObject<ReqAddBusinessPartnerModel>(requestModel);

            await _businessPartnerRepository.UpdateBusinessPartner(reqModel);

            return new ResponseModel<T>();
        }

        public Task<ResponseModel<T>> ProcessBusinessPartner<T>(IRequestModel requestModel)
        {
            return ExecuteComponentAsync<T>(requestModel);
        }

        protected override bool HandleError(IRequestModel requestModel, Exception ex)
        {
            if (ex is PermDuplicateRecordException recordException)
                if (recordException.IndexName == "IX_BusinessPartner_BusinessPartnerName")
                    throw new PermBusinessException("0008", recordException.Value);

            return true;
        }
    }
}
