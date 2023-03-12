using Perm.Admin.Model;
using Perm.Admin.User.Data.Repository;
using Perm.Common.APIModel;
using Perm.Core.RequestManager.Processor;
using Perm.DataAccessLayer.DataRepository.Core.Model;

namespace Perm.Admin.User.Component.Service;

[Authenticate]
public class GetUserService : ServiceBase
{
    public override string URL => "/api/User";

    public override HttpMethod HttpMethod => HttpMethod.Get;

    private readonly IUserRepository _userRepository;

    public GetUserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
    {
        PaginationList<UserModel> getAllWithUserDefinedFieldAsync = await _userRepository.GetAllWithPagination(applyFilter: true).ToPaginationListAsync();

        return new ResponseModel<T>
        {
            Data = (T)(object)getAllWithUserDefinedFieldAsync
        };
    }
}