using System;
using System.Configuration;

namespace MvcApplication1.Common.Helpers
{
    public class ConfigHelper
    {
        public static T Get<T>(string key)
        {
            return (T)Convert.ChangeType(ConfigurationManager.AppSettings[key],typeof(T));
        }


    }
}
