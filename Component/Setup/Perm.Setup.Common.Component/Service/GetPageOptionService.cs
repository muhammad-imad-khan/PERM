using Perm.Common.APIModel;
using Perm.Core.RequestManager.Processor;
using Perm.DataAccessLayer.DataRepository.Core.Model;
using Perm.Model.Setup;
using Perm.Setup.Common.Data.Repository.Abstraction;

namespace Perm.Setup.Common.Component.Service
{
    /// <summary>
    /// 
    /// </summary>
    [Authenticate]
    public class GetPageOptionService : ServiceBase
    {
        private readonly IPageOptionRepository _pageOptionRepository;

        public GetPageOptionService(IPageOptionRepository pageOptionRepository)
        {
            _pageOptionRepository = pageOptionRepository;
        }

        public override string URL => "/api/PageOption";
        public override HttpMethod HttpMethod => HttpMethod.Get;


        protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            PaginationList<PageOptionModel> pageOptionWithPaginationList = await _pageOptionRepository.GetAllWithPagination(applyFilter: true).ToPaginationListAsync();

            return new ResponseModel<T>
            {
                Data = (T)(object)pageOptionWithPaginationList
            };
        }
    }
}