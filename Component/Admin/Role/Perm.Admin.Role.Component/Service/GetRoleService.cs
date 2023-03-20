using Perm.Admin.Role.Data.Repository;
using Perm.Common.APIModel;
using Perm.Core.RequestManager.Processor;
using Perm.DataAccessLayer.DataRepository.Core.Model;
using Perm.Model.Admin;

namespace Perm.Admin.Role.Component.Service;

[Authenticate]
public class GetRoleService : ServiceBase
{
    public override string URL => "/api/Role";
    public override HttpMethod HttpMethod => HttpMethod.Get;
    private readonly IRoleRepository _roleRepository;

    public GetRoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
    {
        PaginationList<RoleModel> entitiesList = await _roleRepository.GetAllWithPagination(applyFilter: true).ToPaginationListAsync();

        return new ResponseModel<T>
        {
            Data = (T)(object)entitiesList
        };
    }
}