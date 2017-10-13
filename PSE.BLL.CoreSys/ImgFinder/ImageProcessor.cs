using HtmlAgilityPack;
using PSE.BLL.CoreSys.Helpers;
using PSE.BLL.CoreSys.ImgFinder.ImgProcessorImplementations;
using PSE.BLL.WCF.Contracts.DataContracts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Downloader = PSE.BLL.CoreSys.FileDownloader.FileDownloader;

namespace PSE.BLL.CoreSys.ImgFinder
{
    public class ImageProcessor
    {
        private IImgFinder _imgFinder;

        public ImageProcessor(string imgFinder)
        {
            SetImgFinder(imgFinder);
        }

        private void SetImgFinder(string imgFinder)
        {
            var currentAssembly = this.GetType().GetTypeInfo().Assembly;

            // we filter the defined classes according to the interfaces they implement
            var ImgFinderClasses = currentAssembly.DefinedTypes
                .Where(type => type.ImplementedInterfaces
                .Any(inter => inter == typeof(IImgFinder))).ToList();

            var finderClass = 
                ImgFinderClasses.SingleOrDefault(cls =>
                (string)cls.GetProperty("FinderName", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null) == imgFinder);

            // if no imgFinder found fallback to custom processor
            _imgFinder = finderClass != null ? (IImgFinder)Activator.CreateInstance(finderClass): new CustomImgFinder();

        }

        /// <summary>
        /// Returns all img sources in given url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<string[]> FindImgUrls(string url)
        {
            try
            {
                WebResponse response = null;
                string fallbackScheme = string.Empty;

                while (response == null)
                {
                    url = url.ValidateUrl(null, fallbackScheme);
                    HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
                    response = await wr.GetResponseAsync();

                    if (response == null)
                        fallbackScheme = fallbackScheme == string.Empty ? "https" : null;
                    if (fallbackScheme == null)
                        return null;
                }  

                string html = string.Empty;

                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    html = await sr.ReadToEndAsync();

                string[] urls = _imgFinder.GetUrls(html);

                for (int i = 0; i < urls.Length; i++)
                {
                    urls[i] = urls[i].ValidateUrl(url);
                }

                return urls;
            }
            catch(Exception e)
            {
                return null;
            }

        }

        /// <summary>
        /// pulls all images from given url and saves to configured local path
        /// </summary>
        /// <param name="webPageUrl"></param>
        /// <returns></returns>
        public async Task<PullingImagesModel> PullImagesFromWebPage(string webPageUrl)
        {
            // return number of downloaded images instead of bool
            try
            {
                var imagesModel = new PullingImagesModel();
                int downloadCounter = 0;

                var urls = await FindImgUrls(webPageUrl);
                if (urls == null) return imagesModel;

                imagesModel.ImagesCount = urls.Length;

                Parallel.ForEach(urls, (url, state) =>
                 {
                     if (Downloader.DownloadByteStream(url))
                     {
                         Interlocked.Increment(ref downloadCounter);
                     }
                 });
                imagesModel.DownloadedImagesCount = downloadCounter;

                return imagesModel;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// pulls all images from given url and saves to configured local path
        /// </summary>
        /// <param name="webPageUrl"></param>
        /// <returns></returns>
        public async Task<List<FileModel>> ReturnImagesFromWebPage(string webPageUrl)
        {
            // return number of downloaded images instead of bool
            try
            {
                var files = new ConcurrentBag<FileModel>();

                var urls = await FindImgUrls(webPageUrl);
                if (urls == null) return files.ToList();

                Parallel.ForEach(urls, (url, state) =>
                {
                    var fileValues = Downloader.DownloadAndReturnByteStream(url);
                    if (fileValues != null)
                        files.Add(new FileModel
                        {
                            FileName = fileValues.Item1,
                            FileContent = fileValues.Item2
                        });

                });

                return files.ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}
