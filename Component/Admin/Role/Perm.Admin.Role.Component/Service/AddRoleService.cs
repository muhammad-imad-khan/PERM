using Perm.Admin.Role.Component.APIModel;
using Perm.Admin.Role.Data.Repository;
using Perm.Common.APIModel;
using Perm.Core.ExceptionManager;
using Perm.Core.RequestManager.Processor;
using Perm.Core.RequestManager.Processor.PermException;

namespace Perm.Admin.Role.Component.Service;

[Authenticate]
public class AddRoleService : ServiceBase
{
    public override string URL => "/api/Role/Add";
    public override HttpMethod HttpMethod => HttpMethod.Post;
    private readonly IRoleRepository _roleRepository;

    public AddRoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
    {
        ReqAddRoleModel reqModel = CastToObject<ReqAddRoleModel>(requestModel);

        await _roleRepository.AddRole(reqModel);

        return new ResponseModel<T>()
        {
            Data = (T)(object)new ResEntityModel
            {
                EntityID = reqModel.RoleID
            }
        };
    }

    public Task<ResponseModel<T>> ProcessRole<T>(IRequestModel requestModel)
    {
        return ExecuteComponentAsync<T>(requestModel);
    }

    protected override bool HandleError(IRequestModel requestModel, Exception ex)
    {
        if (ex is PermDuplicateRecordException recordException)
            if (recordException.IndexName == "IX_Role_RoleName")
                throw new PermBusinessException("0008", recordException.Value);

        return true;
    }
}