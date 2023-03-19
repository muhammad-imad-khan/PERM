using Perm.Common.APIModel;
using Perm.Core.RequestManager.Processor;
using Perm.DataAccessLayer.DataRepository.Core.Abstraction;
using Perm.DataAccessLayer.DataRepository.Core.Model;
using Perm.Model.Abstraction;

namespace Perm.Config.CustomFieldMeta.Component.Service
{
    public class GetCustomFieldMetaService : ServiceBase
    {
        private ICustomFieldMetaRepository _customFieldMetaRepository;

        public GetCustomFieldMetaService(ICustomFieldMetaRepository customFieldMetaRepository)
        {
            _customFieldMetaRepository = customFieldMetaRepository;
        }

        public override string URL => "api/CustomFieldMeta";

        public override HttpMethod HttpMethod => HttpMethod.Get;

        protected async override Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            PaginationList<CustomFieldMetaModel> getAllWithPagination = await _customFieldMetaRepository.GetAllWithPagination(applyFilter: true).ToPaginationListAsync();

            return new ResponseModel<T>
            {
                Data = (T)(object)getAllWithPagination
            };
        }
    }
}