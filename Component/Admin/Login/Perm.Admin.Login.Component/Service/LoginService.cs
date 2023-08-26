using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Perm.Admin.Login.Component.APIModel;
using Perm.Admin.Login.Component.Helper;
using Perm.Admin.User.Data.Repository;
using Perm.Common;
using Perm.Common.APIModel;
using Perm.Config.ApplicationParamDetail.Data.Repository;
using Perm.Core.ExceptionManager;
using Perm.Core.RequestManager.Processor;
using Perm.Model.Admin;
using Perm.Security.AuthenticateManager;

namespace Perm.Admin.Login.Component.Service;

public class LoginService : ServiceBase
{
    private readonly MenuGenerator _menuGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IApplicationParamDetailRepository _applicationParamDetailRepository;

    public LoginService(
          IUserRepository userRepository
        , IUserRoleRepository userRoleRepository
        , IApplicationParamDetailRepository applicationParamDetailRepository
        , IHttpContextAccessor httpContextAccessor
        , MenuGenerator menuGenerator)
    {
        _menuGenerator = menuGenerator;
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
        _httpContextAccessor = httpContextAccessor;
        _applicationParamDetailRepository = applicationParamDetailRepository;
    }

    public override string URL => "/api/Login";
    public override HttpMethod HttpMethod => HttpMethod.Post;

    protected override Task<ResponseModel<T>> ExecuteComponentAsync<T>(IRequestModel requestModel)
    {
        ReqLoginModel reqLoginModel = CastToObject<ReqLoginModel>(requestModel);

        UserModel userModel = _userRepository
            .GetAll()
            .Include(c => c.UserRole).ThenInclude(x => x.Role)
            .FirstOrDefault(s => s.LoginID == reqLoginModel.Username.ToUpper());

        if (userModel is null)
            throw new PermBusinessException("0003");
        else if (!HashPassword.Verify(reqLoginModel.Password, userModel.Password))
            throw new PermBusinessException("0004");
        else if (userModel.IsActive == false)
            throw new PermBusinessException("0009");

        _httpContextAccessor.HttpContext.Items["UserID"] = userModel.UserID;

        string roleIDs = "0";
        if (userModel.UserRole is not null)
        {
            roleIDs = string.Join(',', userModel.UserRole.Select(s => s.RoleID).ToList());
        }

        string generateToken = AuthenticateHelper.GenerateToken(new Dictionary<string, string>
        {
            {"UserID", userModel.UserID.ParseToString()},
            {"RoleID", roleIDs},
            {"TenantID", _userRepository.TenantConfigModel.TenantID}
        },
        timeout: 30);

        List<ResMenuModel> menuModels = _menuGenerator.Generate(userModel.UserID);

        return Task.FromResult(new ResponseModel<T>
        {
            Data = (T)(object)new ResLoginModel
            {
                Token = generateToken,
                TenantConfig = _userRepository.TenantConfigModel,
                Menu = menuModels,
                Role = userModel.UserRole.Select(s => s.Role.Name).ToList()
            }
        });
    }
}