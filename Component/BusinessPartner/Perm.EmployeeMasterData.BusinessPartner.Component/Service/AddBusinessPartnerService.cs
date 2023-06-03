using Perm.Common.APIModel;
using Perm.Core.ExceptionManager;
using Perm.Core.RequestManager.Processor;
using Perm.Core.RequestManager.Processor.PermException;
using Perm.EmployeeMasterData.BusinessPartner.Component.APIModel;
using Perm.EmployeeMasterData.BusinessPartner.Data.Repository.Abstraction;

namespace Perm.EmployeeMasterData.BusinessPartner.Component.Service
{
    [Authenticate]
    public class AddBusinessPartnerService : ServiceBase
    {
        public override string URL => "/api/BusinessPartner/Add";
        public override HttpMethod HttpMethod => HttpMethod.Post;
        private readonly IBusinessPartnerRepository _businessPartnerRepository;

        public AddBusinessPartnerService(IBusinessPartnerRepository businessPartnerRepository)
        {
            _businessPartnerRepository = businessPartnerRepository;
        }

        protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            ReqAddBusinessPartnerModel reqModel = CastToObject<ReqAddBusinessPartnerModel>(requestModel);

            await _businessPartnerRepository.AddBusinessPartner(reqModel);

            return new ResponseModel<T>()
            {
                Data = (T)(object)new ResEntityModel
                {
                    EntityID = reqModel.BusinessPartnerID
                }
            };
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
