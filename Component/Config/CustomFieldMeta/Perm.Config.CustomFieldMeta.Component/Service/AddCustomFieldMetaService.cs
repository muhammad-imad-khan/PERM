using Perm.Common.APIModel;
using Perm.Config.CustomFieldMeta.Component.APIModel;
using Perm.Core.ExceptionManager;
using Perm.Core.RequestManager.Processor;
using Perm.Core.RequestManager.Processor.PermException;
using Perm.DataAccessLayer.DataRepository.Core.Abstraction;

namespace Perm.Config.CustomFieldMeta.Component.Service
{
    public class AddCustomFieldMetaService : ServiceBase
    {
        private readonly ICustomFieldMetaRepository _customFieldMetaRepository;

        public override string URL => "/api/CustomField/Add";

        public override HttpMethod HttpMethod => HttpMethod.Post;

        public AddCustomFieldMetaService(ICustomFieldMetaRepository customFieldMetaRepository)
        {
            _customFieldMetaRepository = customFieldMetaRepository;
        }

        protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            ReqAddCustomFieldMetaModel reqModel = CastToObject<ReqAddCustomFieldMetaModel>(requestModel);

            await _customFieldMetaRepository.AddCustomFieldMeta(reqModel);

            return new ResponseModel<T>()
            {
                Data = (T)(object)new ResEntityModel
                {
                    EntityID = reqModel.CustomMetaID
                }
            };
        }

        public Task<ResponseModel<T>> ProcessCustomFieldMeta<T>(IRequestModel requestModel)
        {
            return ExecuteComponentAsync<T>(requestModel);
        }

        protected override bool HandleError(IRequestModel requestModel, Exception ex)
        {
            if (ex is PermDuplicateRecordException recordException)
                if (recordException.IndexName == "IX_CustomField_DisplayName")
                    throw new PermBusinessException("0008", recordException.Value);

            return true;
        }
    }
}