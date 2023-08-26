using Perm.Core.TenantManager.Abstraction;

namespace Perm.Admin.Login.Component.APIModel
{
    internal class ResLoginModel
    {
        public string Token { get; set; }
        public ITenantConfigModel TenantConfig { get; set; }
        public List<ResMenuModel> Menu { get; set; }
        public Dictionary<string, string> GlobalSettingDictionary { get; set; }
        public List<string> Role { get; set; }
    }
}
