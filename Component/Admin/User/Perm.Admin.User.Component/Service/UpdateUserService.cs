using Perm.Admin.Model;
using Perm.Admin.User.Component.APIModel;
using Perm.Admin.User.Data.Repository;
using Perm.Common.APIModel;
using Perm.Core.ExceptionManager;
using Perm.Core.RequestManager.Processor;
using Perm.Core.RequestManager.Processor.PermException;

namespace Perm.Admin.User.Component.Service;

[Authenticate]
public class UpdateUserService : ServiceBase
{
    public override string URL => "/api/User/Update";

    public override HttpMethod HttpMethod => HttpMethod.Put;

    private readonly IUserRepository _userRepository;

    public UpdateUserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    protected override async Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
    {
        ReqAddUserModel userModel = CastToObject<ReqAddUserModel>(requestModel);

        #region Restricting Sensitive properties from Updating

        UserModel dbUser = await _userRepository.FirstOrNullAsync(s => s.UserID == userModel.UserID);

        if (dbUser != null)
        {
            userModel.LoginID = dbUser.LoginID;
            userModel.Password = dbUser.Password;
        }

        #endregion

        foreach (UserRoleModel item in userModel.UserRole)
        {
            item.Role = null;
        }

        await _userRepository.UpdateUser(userModel);

        return new ResponseModel<T>(); ;
    }

    protected override bool HandleError(IRequestModel requestModel, Exception ex)
    {
        if (ex is PermDuplicateRecordException recordException)
            if (recordException.IndexName == "IX_User_LoginName")
                throw new PermBusinessException("0008", recordException.Value);

        return true;
    }

    public Task<ResponseModel<T>> ProcessUser<T>(IRequestModel requestModel)
    {
        return ExecuteComponentAsync<T>(requestModel);
    }
}