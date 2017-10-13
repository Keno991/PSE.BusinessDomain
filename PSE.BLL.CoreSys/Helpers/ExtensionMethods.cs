using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PSE.BLL.CoreSys.Helpers
{
    static class ExtensionMethods
    {
        /// <summary>
        /// checks if url is valid, if not constructs and returns the valid one based on given rules
        /// </summary>
        /// <param name="url"></param>
        /// <param name="basePath"></param>
        public static string ValidateUrl(this string url, string basePath = "", string fallbackScheme = "")
        {
            string regexMatch = @"^(http|https)://";
            Regex r = new Regex(regexMatch);

            Uri uri = null;
            if (!string.IsNullOrEmpty(basePath) && r.Match(basePath).Success)
            {
                uri = new Uri(basePath);
                basePath = uri.GetLeftPart(UriPartial.Authority);
            }
            else
                basePath = !string.IsNullOrEmpty(fallbackScheme) ? fallbackScheme : "http";

            Match m = r.Match(url);
            if (!m.Success && uri == null)
                url = $"{basePath}://{url}";
            else if (!m.Success && url.StartsWith(@"//") && uri != null)
                url = $"{uri.Scheme}:{url}";
            else if (!m.Success && url.StartsWith(@"/"))
                url = $@"{basePath}{url}";
            else if (!m.Success)
                url = $@"{basePath}/{url}";

            return url;
        }

    }
}
