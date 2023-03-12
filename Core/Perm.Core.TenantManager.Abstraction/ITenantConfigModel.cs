using System.Text.Json.Serialization;

namespace Perm.Core.TenantManager.Abstraction
{
    public interface ITenantConfigModel
    {
        public string TenantName { get; set; }
        public string TenantID { get; set; }

        [JsonIgnore] public ITenantDataBaseConfigModel TenantDataBaseConfig { get; set; }
    }
}