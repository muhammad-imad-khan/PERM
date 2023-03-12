using Perm.Admin.Model;
using Perm.Admin.User.Component.APIModel;
using Perm.Admin.User.Data.Repository;
using Perm.Common.APIModel;
using Perm.Core.ExceptionManager;
using Perm.Core.RequestManager.Processor;
using Perm.Core.RequestManager.Processor.TCubeException;
using Perm.Security.AuthenticateManager;

namespace Perm.Admin.User.Component.Service;

[Authenticate]
public class AddUserService : ServiceBase
{
    private readonly IUserRepository _userRepository;

    public override string URL => "/api/User/Add";
    public override HttpMethod HttpMethod => HttpMethod.Post;

    public AddUserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
    {
        ReqAddUserModel userModel = CastToObject<ReqAddUserModel>(requestModel);

        foreach (UserRoleModel item in userModel.UserRole)
        {
            item.UserRoleID = 0;
            item.Role = null;
        }

        userModel.LoginID = userModel.LoginID.ToUpper();

        userModel.Password = HashPassword.Hash("123");

        await _userRepository.AddUser(userModel);

        return new ResponseModel<T>()
        {
            Data = (T)(object)new ResEntityModel
            {
                EntityID = userModel.UserID,
                Value = userModel.LoginID
            }
        };
    }

    public Task<ResponseModel<T>> ProcessUser<T>(IRequestModel requestModel)
    {
        return ExecuteComponentAsync<T>(requestModel);
    }

    protected override bool HandleError(IRequestModel requestModel, Exception ex)
    {
        if (ex is PermDuplicateRecordException recordException)
            if (recordException.IndexName == "IX_User_LoginName")
                throw new PermBusinessException("0008", recordException.Value);

        return true;
    }
}
