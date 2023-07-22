using Perm.Common.APIModel;

namespace Perm.Admin.Permission.Component.ApiModel
{
    public class ReqDeletePermissionModel : IRequestModel
    {
        public long PermissionID { get; set; }
    }
}
