using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Perm.Admin.User.Component.APIModel
{
    internal class ResUserAndRoleModel
    {
        public string Name { get; set; }

        public string FullName { get; set; }

        public long ID { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EnumEntityType EntityType { get; set; }

        public enum EnumEntityType
        {
            USER,
            ROLE
        }
    }
}