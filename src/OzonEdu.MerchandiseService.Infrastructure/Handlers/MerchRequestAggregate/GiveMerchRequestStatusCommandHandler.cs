using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Commands.GiveMerchRequest;

namespace OzonEdu.MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate
{
    public class GiveMerchRequestStatusCommandHandler: IRequestHandler<GiveMerchRequestStatusCommand, string>
    {
        private readonly IMerchRequestRepository _repository;

        public GiveMerchRequestStatusCommandHandler(IMerchRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> Handle(GiveMerchRequestStatusCommand request, CancellationToken cancellationToken)
        {
            var merchRequest = await _repository.FindByRequestNumberAsync(new RequestNumber(request.RequestId), cancellationToken);
            if (merchRequest is null)
                throw new Exception($"Not found with id {request.RequestId}");

            return merchRequest.RequestStatus.Name;
        }
    }
}