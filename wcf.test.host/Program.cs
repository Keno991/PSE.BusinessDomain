
using PSE.BLL.CoreSys.FileDownloader;
using PSE.BLL.CoreSys.Helpers;
using PSE.BLL.CoreSys.ImgFinder;
using PSE.BLL.WCF.Services.Implementations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace wcf.test.host
{
    class Program
    {

        static void Main(string[] args)
        {

            //FtpWebRequest req = FtpWebRequest.Create(@"‪C:\Users\User-PC\Desktop\pitanja.txt") as FtpWebRequest;

            //req.Method = "GET";

            //using (var response = req.GetResponse())
            //{
            //    using (var reader = new StreamReader(response.GetResponseStream()))
            //    {
            //        Console.WriteLine(reader.ReadToEnd());
            //    }
            //}
            Uri u = new Uri("www.test.com");
            Console.WriteLine(u);
        }

    }
}
