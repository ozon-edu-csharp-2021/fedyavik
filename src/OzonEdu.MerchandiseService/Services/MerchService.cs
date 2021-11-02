using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Models;

namespace OzonEdu.MerchandiseService.Services
{
    public class MerchService: IMerchService
    {
        private List<RequestMerchModel> RequestMerchModels = new List<RequestMerchModel>()
        {
            new RequestMerchModel(1, "")
        };

        public Task<RequestMerchModel> RequestMerch(string merchName, CancellationToken token)
        {
            var requestId = RequestMerchModels.Max(x => x.RequestId) + 1;
            var newMerchRequest = new RequestMerchModel(requestId, "");
            RequestMerchModels.Add(newMerchRequest);
            return Task.FromResult(newMerchRequest);
        }

        public Task<RequestMerchModel> GetIssuingMerchInfo(long requestId, CancellationToken _)
        {
            var requestMerch = RequestMerchModels.FirstOrDefault(
                x => x.RequestId == requestId);
            return Task.FromResult(requestMerch);
        }
    }
}