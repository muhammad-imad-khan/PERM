using Perm.Common.APIModel;
using Perm.Core.RequestManager.Processor;
using Perm.EmployeeMasterData.BusinessPartner.Component.APIModel;
using Perm.EmployeeMasterData.BusinessPartner.Data.Repository.Abstraction;

namespace Perm.EmployeeMasterData.BusinessPartner.Component.Service
{
    [Authenticate]
    public class AddBusinessPartnerFeedback : ServiceBase
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public AddBusinessPartnerFeedback(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public override string URL => "/api/BusinessPartner/Feedback";
        public override HttpMethod HttpMethod => HttpMethod.Post;

        protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            ReqAddFeedbackRequestModel reqModel = CastToObject<ReqAddFeedbackRequestModel>(requestModel);

            await _feedbackRepository.AddFeedback(reqModel);

            return new ResponseModel<T>();
        }
    }
}
