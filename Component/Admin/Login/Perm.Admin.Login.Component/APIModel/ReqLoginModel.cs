using Perm.Common.APIModel;

namespace Perm.Admin.Login.Component.APIModel
{
    internal class ReqLoginModel : IRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}