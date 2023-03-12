using System.Globalization;

namespace Perm.Common
{
    public static class Parse
    {
        public static T ParseTo<T>(this object obj) where T : IConvertible
        {
            if (obj != null)
            {
                string str = obj.ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    if (typeof(T) == typeof(string))
                        return (T)Convert.ChangeType(str, typeof(T));

                    if (typeof(T) == typeof(int))
                    {
                        int.TryParse(str, out int t);
                        return (T)Convert.ChangeType(t, typeof(T));
                    }

                    if (typeof(T) == typeof(long))
                    {
                        long.TryParse(str, out long t);
                        return (T)Convert.ChangeType(t, typeof(T));
                    }
                    if (typeof(T) == typeof(float))
                    {
                        float.TryParse(str, out float t);
                        return (T)Convert.ChangeType(t, typeof(T));
                    }
                    if (typeof(T) == typeof(double))
                    {
                        double.TryParse(str, out double t);
                        return (T)Convert.ChangeType(t, typeof(T));
                    }
                    if (typeof(T) == typeof(decimal))
                    {
                        decimal.TryParse(str, out decimal t);
                        return (T)Convert.ChangeType(t, typeof(T));
                    }
                    if (typeof(T) == typeof(char))
                    {
                        char.TryParse(str, out char t);
                        return (T)Convert.ChangeType(t, typeof(T));
                    }
                    if (typeof(T) == typeof(bool))
                    {
                        bool t = (str == "1" || str.ToLower() == "true");
                        return (T)Convert.ChangeType(t, typeof(T));
                    }

                    if (typeof(T) == typeof(DateTime))
                    {
                        DateTime.TryParseExact(str, new string[] { "yyyy-MM-dd", "yyyy-MM-dd hh:mm:ss", "yyyy-MM-dd hh:mm:ss.fff", Constant.JSON_DATE_TIME_FORMAT }, null, DateTimeStyles.None,
                            out DateTime t);
                        return (T)Convert.ChangeType(t, typeof(T));
                    }

                    if (typeof(T) == typeof(Enum))
                    {

                        T foo = (T)Enum.ToObject(typeof(T), obj.ParseTo<int>());
                        //long.TryParse(str, out long t);
                        return (T)Convert.ChangeType(foo, typeof(T));
                    }

                    if (typeof(T).BaseType == typeof(Enum))
                    {
                        if (Enum.TryParse(typeof(T), str, true, out object tt))
                            return (T)Convert.ChangeType(tt, typeof(T));
                        return default;
                    }
                }
            }

            return default;
        }
    }
}