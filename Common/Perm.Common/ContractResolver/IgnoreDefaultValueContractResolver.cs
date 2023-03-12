using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace Perm.Common.ContractResolver
{
    public class IgnoreDefaultValueContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty prop = base.CreateProperty(member, memberSerialization);
            if (prop.PropertyType == typeof(long))
            {
                prop.DefaultValue = 0;
            }
            if (prop.PropertyType == typeof(bool))
            {
                prop.DefaultValue = false;
            }
            if (prop.PropertyName == "CreatedOn")
                prop.Ignored = true;

            prop.DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate;
            return prop;
        }
    }
}
