using PSE.BLL.WCF.Contracts.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PSE.BLL.WCF.Contracts.Contracts
{
    [ServiceContract]
    public interface IImageUtilityService
    {
        [OperationContract]
        Task<PullingImagesModel> PullImagesFromWebPage(string webUrl);
        [OperationContract]
        Task<List<FileModel>> PullFilesFromWebPage(string webUrl);

    }
}
