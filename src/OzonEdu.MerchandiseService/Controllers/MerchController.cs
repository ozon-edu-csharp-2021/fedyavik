using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchItemAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Commands.CreateMerchRequest;
using OzonEdu.MerchandiseService.Infrastructure.Commands.GiveMerchRequest;
using OzonEdu.MerchandiseService.Models;
using OzonEdu.MerchandiseService.Services;

namespace OzonEdu.MerchandiseService.Controllers
{
    [Route("v1/api/merch")]
    public class MerchController: ControllerBase
    {
        private readonly IMerchService _merchService;
        private readonly IMediator _mediator;
        public MerchController(IMerchService merchService, IMediator mediator)
        {
            _merchService = merchService;
            _mediator = mediator;
        }
        [HttpGet("{id:long}")]
        public async Task<ActionResult<RequestMerchModel>> GetRequestMerchInfo(long id, CancellationToken token)
        {
            var giveMerchRequestStatusCommand = new GiveMerchRequestStatusCommand()
            {
                RequestId = id
            };
            var requestMerchStatus = await _mediator.Send(giveMerchRequestStatusCommand, token);

            var createMerchRequest = new RequestMerchModel(id, requestMerchStatus);
            return createMerchRequest;
        }
        
        [HttpPost]
        public async Task<ActionResult<RequestMerchModel>> RequestMerch(
            RequestMerchPostViewModel postModel, CancellationToken token)
        {
            var createMerchRequestCommand = new CreateMerchRequestCommand()
            {
                EmployeeId = postModel.EmployeeId,
                Sku = postModel.Sku
            };

            var result = await _mediator.Send(createMerchRequestCommand, token);
            
            var createMerchRequest = new RequestMerchModel(result, RequestStatus.InWork.Name);
            return Ok(createMerchRequest);
        }
    }
}