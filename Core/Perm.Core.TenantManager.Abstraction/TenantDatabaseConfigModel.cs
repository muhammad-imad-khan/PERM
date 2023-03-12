using Perm.Core.TenantManager.Abstraction;

namespace Perm.Core.TenantManager.Abstraction
{
    public class TenantDataBaseConfigModel : ITenantDataBaseConfigModel
    {
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}