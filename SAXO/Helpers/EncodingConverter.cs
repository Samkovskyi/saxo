using System;
using System.Text;

namespace SAXO.Helpers
{
    public static class EncodingConverter
    {
        private static Encoding iso = Encoding.GetEncoding("ISO-8859-1");
        private static Encoding utf8 = Encoding.UTF8;

        public static String Convert(String str)
        {
            var utfBytes = utf8.GetBytes(str);
            var isoBytes = Encoding.Convert(utf8, iso, utfBytes);
            var result = utf8.GetString(isoBytes);
            return (!String.IsNullOrEmpty(result)) ? result : str;
        }
    }
}