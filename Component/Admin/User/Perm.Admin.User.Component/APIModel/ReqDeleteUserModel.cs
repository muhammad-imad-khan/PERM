using Perm.Common.APIModel;

namespace Perm.Admin.User.Component.APIModel
{
    internal class ReqDeleteUserModel : IRequestModel
    {
        public long UserID { get; set; }
    }
}
