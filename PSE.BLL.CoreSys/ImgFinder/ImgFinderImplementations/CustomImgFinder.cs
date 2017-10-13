using PSE.BLL.CoreSys.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSE.BLL.CoreSys.ImgFinder.ImgProcessorImplementations
{
    internal class CustomImgFinder : IImgFinder
    {

        static string FinderName => "Custom";


        public string[] GetUrls(string html)
        {
            int position = default(int);

            List<string> lst = new List<string>();
            while (position > -1)
            {
                if ((position = html.IndexOf("src", position)) > -1)
                {
                    int substringLength = html.IndexOf("\"", position += 5) - position;

                    string url = html.Substring(position, substringLength);

                    lst.Add(url);
                }
            }

            return lst.ToArray();
        }
    }
}
