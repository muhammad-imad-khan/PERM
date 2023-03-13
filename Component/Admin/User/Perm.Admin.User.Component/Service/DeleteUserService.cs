using Perm.Admin.User.Component.APIModel;
using Perm.Admin.User.Data.Repository;
using Perm.Common.APIModel;
using Perm.Core.ExceptionManager;
using Perm.Core.RequestManager.Processor;
using Perm.Core.RequestManager.Processor.PermException;

namespace Perm.Admin.User.Component.Service;

[Authenticate]
public class DeleteUserService : ServiceBase
{
    public override string URL => "/api/User/Delete";

    private readonly IUserRepository _userRepository;

    public override HttpMethod HttpMethod => HttpMethod.Delete;

    public DeleteUserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
    {
        ReqDeleteUserModel userModel = CastToObject<ReqDeleteUserModel>(requestModel);

        await _userRepository.DeleteUser(userModel.UserID);

        return new ResponseModel<T>();
    }
    protected override bool HandleError(IRequestModel requestModel, Exception ex)
    {
        if (ex is PermDuplicateRecordException)
            throw new PermBusinessException("0003");

        return true;
    }

    public Task<ResponseModel<T>> ProcessUser<T>(IRequestModel requestModel)
    {
        return ExecuteComponentAsync<T>(requestModel);
    }
}