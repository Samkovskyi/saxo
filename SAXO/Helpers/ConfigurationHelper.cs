using System;
using System.Configuration;

namespace SAXO.Helpers
{
    public static class ConfigurationHelper
    {
        public static String GetBooksApiUrl()
        {
            return ConfigurationManager.AppSettings.Get("SAXO:BooksApi");
        }

        public static String GetApiKey()
        {
            return ConfigurationManager.AppSettings.Get("SAXO:ApiKey");
        }
    }
}