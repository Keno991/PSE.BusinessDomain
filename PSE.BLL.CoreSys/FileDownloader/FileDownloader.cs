using PSE.BLL.CoreSys.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PSE.BLL.CoreSys.FileDownloader
{
    public static class FileDownloader
    {

        public static bool DownloadFile(string path)
        {
            try
            {

                Uri uri = new Uri(path);
                string fileName = uri.Segments[uri.Segments.Length - 1];

                string destinationPath = $"{ConfigurationHelper.GetConfigValue("DownloadFileLocation")}\\{fileName}";

                using (var client = new WebClient())
                {
                    client.DownloadFile(new Uri(path), destinationPath);
                }
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Downloads byte array and creates corresponding file locally
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool DownloadByteStream(string path)
        {

            try
            {

                Uri uri = new Uri(path);
                string fileName = uri.Segments[uri.Segments.Length - 1];

                string destinationPath = Path.Combine(ConfigurationHelper.GetConfigValue("DownloadFileLocation"), fileName);

                using (var client = new WebClient())
                {
                    using (var MemoryStream = new MemoryStream(client.DownloadData(new Uri(path))))
                    {
                        using (var fileStream = new FileStream(destinationPath, FileMode.Create))
                        {
                            MemoryStream.WriteTo(fileStream);
                            MemoryStream.Flush();
                        }
                    }
                }
                return true;
            }
            catch(Exception e) { return false; }

        }

        /// <summary>
        /// Downloads file as byte array and returns tuple containing file name and content
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Tuple<string, byte[]> DownloadAndReturnByteStream(string path)
        {
            try
            {

                Uri uri = new Uri(path);
                string fileName = uri.Segments[uri.Segments.Length - 1];

                using (var client = new WebClient())
                {
                    return new Tuple<string, byte[]>(fileName, client.DownloadData(path));
                }
            }
            catch (Exception e) { return null; }
        }

    }
}
