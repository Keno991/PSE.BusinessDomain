using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace PSE.BLL.CoreSys.Helpers
{
    public static class ConfigurationHelper
    {

        public static T GetConfigValue<T>(string key)
        {
            try
            {
                string value = ConfigurationSettings.AppSettings[key];

                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch(Exception e)
            {
                return default(T);
            }
        }

        public static string GetConfigValue(string key)
        {
              return ConfigurationSettings.AppSettings[key];
        }

    }
}
