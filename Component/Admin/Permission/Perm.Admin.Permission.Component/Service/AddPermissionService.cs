using Perm.Admin.Permission.Component.ApiModel;
using Perm.Admin.Permission.Data.Repository.Abstraction;
using Perm.Common;
using Perm.Common.APIModel;
using Perm.Core.RequestManager.Processor;

namespace Perm.Admin.Permission.Component.Service
{
    [Authenticate]
    public class AddPermissionService : ServiceBase
    {
        private readonly IPermissionRepository _permissionRepository;

        public AddPermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public override HttpMethod HttpMethod => HttpMethod.Get;
        public override string URL => "/api/Permission/Add";

        protected async override Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
        {
            ReqAddPermissionModel reqModel = requestModel.MapTo<ReqAddPermissionModel>();

            await _permissionRepository.AddPermission(reqModel);

            return new ResponseModel<T>
            {
                Data = (T)(object)new ResEntityModel
                {
                    EntityID = reqModel.PermissionID
                }
            };
        }
    }
}