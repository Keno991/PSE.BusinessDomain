using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSE.BLL.CoreSys.ImgFinder.ImgProcessorImplementations
{
    internal class ImageFinderWithHtmlAgilityPack : IImgFinder
    {

        static string FinderName => "HtmlAgilityPackProcessor";


        public string[] GetUrls(string html)
        {
            List<string> lst = new List<string>();

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            return document?.DocumentNode.SelectNodes("//img")
                .Select(img => img.Attributes["src"]?.Value).ToArray();
        }
    }
}
