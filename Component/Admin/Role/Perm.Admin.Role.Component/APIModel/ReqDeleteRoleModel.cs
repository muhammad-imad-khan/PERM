using Perm.Common.APIModel;

namespace Perm.Admin.Role.Component.APIModel
{
    public class ReqDeleteRoleModel : IRequestModel
    {
        public long RoleID { get; set; }
    }
}