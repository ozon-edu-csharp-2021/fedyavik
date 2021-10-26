using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Models;

namespace OzonEdu.MerchandiseService.Services
{
    public interface IMerchService
    {
        Task<RequestMerchModel> RequestMerch(string merchName, CancellationToken token);
        Task<RequestMerchModel> GetIssuingMerchInfo(long requestId, CancellationToken _);
    }
}