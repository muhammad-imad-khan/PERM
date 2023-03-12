using System.Text.Json.Serialization;

namespace Perm.Core.TenantManager.Abstraction
{
    public class TenantConfigModel : ITenantConfigModel
    {
        public string TenantName { get; set; }
        public string TenantID { get; set; }

        [JsonIgnore]
        public ITenantDataBaseConfigModel TenantDataBaseConfig { get; set; } = new TenantDataBaseConfigModel();

        public Dictionary<string, string> Paths { get; set; }

        public TenantThemeModel TenantTheme { get; set; } = new();
    }
}