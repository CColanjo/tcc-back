using System.Text;

namespace System;

public static class StringExt
    {
        /// <summary>
        /// Reduce the string to the supplied maxLength.
        /// </summary>
        /// <param name="maxLength"></param>
        /// <returns>Value truncated</returns>
        public static string RemoveAllSpecialCharacters(this string value, bool removeTrace = false, bool removeSpace = false, bool removeComma = false)
        {
            if (value == null)
                return null;

            value = value.ToLower().Trim();

            value = value.Replace(@"â", @"a");
            value = value.Replace(@"ä", @"a");
            value = value.Replace(@"ã", @"a");
            value = value.Replace(@"à", @"a");
            value = value.Replace(@"á", @"a");

            value = value.Replace(@"ê", @"e");
            value = value.Replace(@"ë", @"e");
            value = value.Replace(@"è", @"e");
            value = value.Replace(@"é", @"e");

            value = value.Replace(@"î", @"i");
            value = value.Replace(@"ï", @"i");
            value = value.Replace(@"ì", @"i");
            value = value.Replace(@"í", @"i");

            value = value.Replace(@"ô", @"o");
            value = value.Replace(@"ö", @"o");
            value = value.Replace(@"õ", @"o");
            value = value.Replace(@"ò", @"o");
            value = value.Replace(@"ó", @"o");

            value = value.Replace(@"û", @"u");
            value = value.Replace(@"ü", @"u");
            value = value.Replace(@"ù", @"u");
            value = value.Replace(@"ú", @"u");

            value = value.Replace(@"ª", @"a");
            value = value.Replace(@"ç", @"c");
            value = value.Replace(@"º", @"o");
            value = value.Replace(@"&", @"y");
            value = value.Replace(@"ñ", @"n");

            var sb = new StringBuilder();

            foreach (char c in value)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == ' ' || c == ',' || c == '-')
                {
                    if (removeTrace && c == '-')
                        continue;

                    if (removeSpace && c == ' ')
                        continue;

                    if (removeComma && c == ',')
                        continue;

                    sb.Append(c);
                }
            }

            var responseString = sb.ToString().ToUpper();

            return responseString;
        }

        /// <summary>
        /// Reduce the string to the supplied maxLength.
        /// </summary>
        /// <param name="maxLength"></param>
        /// <returns>Value truncated</returns>
        public static string Truncate(this string value, int maxLength)
        {
            if (value == null || value.Length <= maxLength)
                return value;

            return value.Substring(0, maxLength);
        }

        /// <summary>
        /// Returns true whether the value is null or empty ignoring the white spaces in accordance of supplied parameter.
        /// </summary>
        public static bool NullOrEmpty(this string value, bool ignoreWhiteSpace = false)
        {
            if (ignoreWhiteSpace)
                return value == null || value.Trim() == String.Empty;

            return String.IsNullOrEmpty(value);
        }
    }