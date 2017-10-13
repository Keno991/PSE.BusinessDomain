using PSE.BLL.CoreSys.Enumerations;
using PSE.BLL.CoreSys.ImgFinder;
using PSE.BLL.WCF.Contracts.Contracts;
using PSE.BLL.WCF.Contracts.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PSE.BLL.WCF.Services.Implementations
{
    public class ImageUtilityService : IImageUtilityService
    {
        public async Task<PullingImagesModel> PullImagesFromWebPage(string webUrl)
        {
            ImageProcessor finder = new ImageProcessor(ImageFinderImplementations.HtmlAgilityPackProcessor.ToString());

            return await finder.PullImagesFromWebPage(webUrl);
        }

        public async Task<List<FileModel>> PullFilesFromWebPage(string webUrl)
        {
            ImageProcessor finder = new ImageProcessor(ImageFinderImplementations.HtmlAgilityPackProcessor.ToString());

            return await finder.ReturnImagesFromWebPage(webUrl);
        }
    }
}