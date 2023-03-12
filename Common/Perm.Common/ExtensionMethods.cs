using Mapster;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Perm.Common;

namespace Perm.Common
{
    public static class ExtensionMethods
    {
        public static T Deserialize<T>(this string str, bool handleCycle = false)
        {
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
            if (handleCycle)
            {
                jsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            }

            return JsonSerializer.Deserialize<T>(str, jsonSerializerOptions);
        }

        public static object Deserialize(this string str, Type type, bool handleCycle = false)
        {
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
            if (handleCycle)
            {
                jsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            }

            return JsonSerializer.Deserialize(str, type, jsonSerializerOptions);
        }

        public static string Serialize(this object obj, bool handleCycle = false)
        {
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
            if (handleCycle)
            {
                jsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            }


            return JsonSerializer.Serialize(obj, options: jsonSerializerOptions);
        }

        public static DateTime ToSystemTimeZone(this DateTime dateTime)
        {
            return dateTime.ToUniversalTime();
        }

        public static string ParseToString(this object obj)
        {
            if (obj is DateTime dateTime)
            {
                return dateTime.ToString(Constant.JSON_DATE_TIME_FORMAT);
            }

            return obj.ToString();
        }
        public static T MapTo<T>(this object source) where T : new()
        {
            TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);

            return source.Adapt<T>();
        }
        public static T MapTo<T>(this object source, bool ignoreCase) where T : new()
        {
            TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);

            if (ignoreCase)
            {
                TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);
            }

            return source.Adapt<T>();
        }

        public static long GetDigits(this string str)
        {
            return Regex.Match(str, @"\d+").Value.ParseTo<long>();
        }

        public static string SplitCamelCase(this string str)
        {
            return Regex.Replace(str, "([A-Z])", " $1").Trim();
        }

        public static string Zip(this object obj)
        {
            string jsonObject = JsonConvert.SerializeObject(obj, null, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            // string jsonObject = obj.Serialize();
            return jsonObject.Zip();
        }

        public static string Zip(this string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);

            using var msi = new MemoryStream(bytes);
            using var mso = new MemoryStream();
            using (var gs = new GZipStream(mso, CompressionMode.Compress))
            {
                msi.CopyTo(gs);
            }

            byte[] zipBytes = mso.ToArray();
            return Convert.ToBase64String(zipBytes);
        }

        public static string Unzip(this string zipped)
        {
            byte[] bytes = Convert.FromBase64String(zipped);
            using var msi = new MemoryStream(bytes);
            using var mso = new MemoryStream();
            using (var gs = new GZipStream(msi, CompressionMode.Decompress))
            {
                gs.CopyTo(mso);
            }

            return Encoding.UTF8.GetString(mso.ToArray());
        }

        public static string LastOrNonEmpty(this IEnumerable<string> source)
        {
            IEnumerable<string> enumerable = source as string[] ?? source.ToArray();
            string lastOrDefault = enumerable.LastOrDefault();
            if (string.IsNullOrEmpty(lastOrDefault))
            {
                return enumerable.LastOrDefault(val => !string.IsNullOrEmpty(val)) + ".";
            }
            return lastOrDefault;
        }

        public static object GetPropertyValue(this string propName, object src)
        {
            if (src == null) throw new ArgumentException("Value cannot be null.", "src");
            if (propName == null) throw new ArgumentException("Value cannot be null.", "propName");

            if (propName.Contains("."))//complex type nested
            {

                string[] temp = propName.Split(new char[] { '.' });

                PropertyInfo prop = src.GetType().GetProperty(temp.FirstOrDefault());
                object value = prop != null ? prop.GetValue(src, null) : null;

                return GetPropertyValue(String.Join('.', temp.Skip(1)), value);
            }
            else
            {
                var prop = src.GetType().GetProperty(propName);
                return prop != null ? prop.GetValue(src, null) : null;
            }
        }

        public static void RemoveRange<T>(this List<T> list, List<T> listToRemove) where T : class
        {
            foreach (T entity in listToRemove)
            {
                list.Remove(entity);
            }
        }
    }
}