using Perm.Admin.User.Component.APIModel;
using Perm.Admin.User.Data.Repository;
using Perm.Common.APIModel;
using Perm.Core.RequestManager.Processor;
using Perm.DataAccessLayer.DataRepository.Core.Model;

namespace Perm.Admin.User.Component.Service;

[Authenticate]
public class GetUserAndRoleService : ServiceBase
{
    public override string URL => "/api/UserAndRole";

    public override HttpMethod HttpMethod => HttpMethod.Get;

    private readonly IUserRepository _userRepository;
    //private readonly IRoleRepository _roleRepository;

    public GetUserAndRoleService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        //_roleRepository = roleRepository;
    }

    protected override Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
    {
        List<ResUserAndRoleModel> resUserAndRoleModels = _userRepository.GetAll().Select(s => new ResUserAndRoleModel
        {
            EntityType = ResUserAndRoleModel.EnumEntityType.USER,
            ID = s.UserID,
            Name = s.LoginID,
            FullName = $"{s.FirstName} {s.LastName}"

        }).ToList();

        //resUserAndRoleModels.AddRange(_roleRepository.GetAll().Select(r => new ResUserAndRoleModel
        //{
        //    EntityType = ResUserAndRoleModel.EnumEntityType.ROLE,
        //    FullName = r.Name,
        //    Name = r.Name,
        //    ID = r.RoleID

        //}).ToList());

        PaginationList<ResUserAndRoleModel> paginationList = new PaginationList<ResUserAndRoleModel>
        {
            List = resUserAndRoleModels
        };

        return Task.FromResult(new ResponseModel<T>
        {
            Data = (T)(object)paginationList
        });
    }
}