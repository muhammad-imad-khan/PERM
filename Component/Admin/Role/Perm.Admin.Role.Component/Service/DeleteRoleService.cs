using Perm.Admin.Role.Component.APIModel;
using Perm.Admin.Role.Data.Repository;
using Perm.Common.APIModel;
using Perm.Core.RequestManager.Processor;

namespace Perm.Admin.Role.Component.Service;

[Authenticate]
public class DeleteRoleService : ServiceBase
{
    public override string URL => "/api/Role/Delete";
    public override HttpMethod HttpMethod => HttpMethod.Delete;
    private readonly IRoleRepository _roleRepository;

    public DeleteRoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
    {
        ReqDeleteRoleModel reqModel = CastToObject<ReqDeleteRoleModel>(requestModel);

        await _roleRepository.DeleteRole(reqModel.RoleID);

        return new ResponseModel<T>();
    }

    public Task<ResponseModel<T>> ProcessRole<T>(IRequestModel requestModel)
    {
        return ExecuteComponentAsync<T>(requestModel);
    }
}