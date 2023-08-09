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
    public class GetMenuService : ServiceBase
    {
        private readonly IMenuRepository _menuRepository;

        /// <inheritdoc/>
        public GetMenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public override string URL => "/api/Menu";
        public override HttpMethod HttpMethod => HttpMethod.Get;

        protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            PaginationList<MenuModel> menuWithPaginationList = await _menuRepository.GetAllWithPagination(applyFilter: true).ToPaginationListAsync();

            return new ResponseModel<T>
            {
                Data = (T)(object)menuWithPaginationList
            };
        }
    }
}