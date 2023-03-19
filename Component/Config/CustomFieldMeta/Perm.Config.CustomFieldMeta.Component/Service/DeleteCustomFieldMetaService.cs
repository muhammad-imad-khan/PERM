using Perm.Common.APIModel;
using Perm.Config.CustomFieldMeta.Component.APIModel;
using Perm.Core.RequestManager.Processor;
using Perm.DataAccessLayer.DataRepository.Core.Abstraction;

namespace Perm.Config.CustomFieldMeta.Component.Service
{
    public class DeleteCustomFieldMetaService : ServiceBase
    {
        private readonly ICustomFieldMetaRepository _customFieldMetaRepository;

        public override string URL => "/api/CustomField/Delete";

        public override HttpMethod HttpMethod => HttpMethod.Delete;

        public DeleteCustomFieldMetaService(ICustomFieldMetaRepository customFieldMetaRepository)
        {
            _customFieldMetaRepository = customFieldMetaRepository;
        }

        protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            ReqDeleteCustomFieldMetaModel reqModel = CastToObject<ReqDeleteCustomFieldMetaModel>(requestModel);

            await _customFieldMetaRepository.DeleteCustomFieldMeta(reqModel.CustomFieldMetaID);

            return new ResponseModel<T>();
        }

        public Task<ResponseModel<T>> ProcessCustomFieldMeta<T>(IRequestModel requestModel)
        {
            return ExecuteComponentAsync<T>(requestModel);
        }
    }
}