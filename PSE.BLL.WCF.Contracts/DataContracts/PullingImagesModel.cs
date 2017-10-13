using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PSE.BLL.WCF.Contracts.DataContracts
{
    [DataContract]
    public class PullingImagesModel
    {
        [DataMember]
        public int ImagesCount { get; set; }
        [DataMember]
        public int DownloadedImagesCount { get; set; }
    }
}
