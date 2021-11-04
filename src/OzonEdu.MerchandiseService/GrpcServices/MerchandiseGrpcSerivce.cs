using System.Threading.Tasks;
using Grpc.Core;
using OzonEdu.MerchandiseService.Grpc;
using OzonEdu.MerchandiseService.Services;

namespace OzonEdu.MerchandiseService.GrpcServices
{
    public class MerchandiseGrpcSerivce: MerchandiseServiceGrpc.MerchandiseServiceGrpcBase
    {
        private readonly IMerchService _merchService;

        public MerchandiseGrpcSerivce(IMerchService merchService)
        {
            _merchService = merchService;
        }

        public override async Task<MerchItemResponse> RequestMerch(AddRequestMerch request, ServerCallContext context)
        {
            var merchItem = await _merchService.RequestMerch(request.ItemName, context.CancellationToken);
            return new MerchItemResponse
            {
                RequestId = merchItem.RequestId
            };
        }

        public override async Task<MerchItemResponse> GetRequestMerchInfo(GetMerchInfoRequest request, ServerCallContext context)
        {
            var merchItem = await _merchService.GetIssuingMerchInfo(request.RequestId, context.CancellationToken);
            return new MerchItemResponse
            {
                RequestId = merchItem.RequestId
            };
        }
    }
}