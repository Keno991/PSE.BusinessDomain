using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSE.BLL.CoreSys.ImgFinder
{
    public interface IImgFinder
    { 
        string[] GetUrls(string html);
    }
}
