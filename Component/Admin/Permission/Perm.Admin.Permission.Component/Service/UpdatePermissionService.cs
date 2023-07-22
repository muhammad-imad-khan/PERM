using Perm.Admin.Permission.Component.ApiModel;
using Perm.Admin.Permission.Data.Repository.Abstraction;
using Perm.Common;
using Perm.Common.APIModel;
using Perm.Core.RequestManager.Processor;

namespace Perm.Admin.Permission.Component.Service
{
    [Authenticate]
    public class UpdatePermissionService : ServiceBase
    {
        private readonly IPermissionRepository _permissionRepository;

        public UpdatePermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public override HttpMethod HttpMethod => HttpMethod.Get;
        public override string URL => "/api/Permission/Update";

        protected async override Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            ReqAddPermissionModel reqModel = requestModel.MapTo<ReqAddPermissionModel>();

            await _permissionRepository.UpdatePermission(reqModel);

            return new ResponseModel<T>();
        }
    }
}
