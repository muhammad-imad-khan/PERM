namespace Perm.Common
{
    public static class StringHelper
    {
        /// <summary>
        /// Get string between two strings in a string
        /// </summary>
        /// <param name="source"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string GetBetweenString(this string source, string start, string end)
        {
            int startIndex = source.IndexOf(start, StringComparison.Ordinal);

            if (startIndex < 0)
            {
                return string.Empty;
            }

            string tableName = source.Substring(startIndex + 1, source.IndexOf(end, startIndex, StringComparison.Ordinal) - startIndex - 1);

            return tableName;
        }

        /// <summary>
        /// A wrapper of String.Equals with trim
        /// </summary>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SameAs(this string source, string value)
        {
            if (source != null)
            {
                if (value is null)
                    value = string.Empty;

                return source.Trim().Equals(value.Trim(), StringComparison.OrdinalIgnoreCase);
            }
            else
                return false;
        }

        /// <summary>
        /// A wrapper of String.Equals with trim
        /// </summary>
        /// <param name="source"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool SameAsAny(this string source, string[] values)
        {
            if (source != null)
            {
                values ??= new string[] { };
                return values.Any(value => !string.IsNullOrEmpty(value) && source.Trim().Equals(value.Trim(), StringComparison.OrdinalIgnoreCase));
            }

            return false;
        }

        /// <summary>
        /// Get only alphabets from string
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToAlphabets(this string source)
        {
            return new string(source.Where(char.IsLetter).ToArray());
        }
    }
}