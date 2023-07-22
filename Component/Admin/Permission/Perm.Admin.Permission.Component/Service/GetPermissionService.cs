using Perm.Admin.Permission.Data.Repository.Abstraction;
using Perm.Common.APIModel;
using Perm.Core.RequestManager.Processor;
using Perm.DataAccessLayer.DataRepository.Core.Model;
using Perm.Model.Admin;

namespace Perm.Admin.Permission.Component.Service
{
    [Authenticate]
    public class GetPermissionService : ServiceBase
    {
        private readonly IPermissionRepository _permissionRepository;

        public GetPermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public override HttpMethod HttpMethod => HttpMethod.Get;
        public override string URL => "/api/Permission";

        protected async override Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            PaginationList<PermissionModel> permissionWithPagination = await _permissionRepository.GetAllWithPagination(applyFilter: true).ToPaginationListAsync();

            return new ResponseModel<T>
            {
                Data = (T)(object)permissionWithPagination
            };
        }
    }
}
