using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PSE.BLL.WCF.Contracts.DataContracts
{
    [DataContract]
    public class FileModel
    {
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public byte[] FileContent { get; set; }

    }
}
