using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.MerchandiseService.Models;
using OzonEdu.MerchandiseService.Services;

namespace OzonEdu.MerchandiseService.Controllers
{
    [Route("v1/api/merch")]
    public class MerchController: ControllerBase
    {
        private readonly IMerchService _merchService;

        public MerchController(IMerchService merchService)
        {
            _merchService = merchService;
        }
        [HttpGet("{id:long}")]
        public async Task<ActionResult<RequestMerchModel>> GetRequestMerchInfo(long id, CancellationToken token)
        {
            var requestMerch = await _merchService.GetIssuingMerchInfo(id, token);
            if (requestMerch is null)
                return NotFound();
            return requestMerch;
        }
        
        [HttpPost]
        public async Task<ActionResult<RequestMerchModel>> RequestMerch(
            RequestMerchPostViewModel postModel, CancellationToken token)
        {
            var createMerchRequest = await _merchService.RequestMerch(postModel.ItemName, token);
            return Ok(createMerchRequest);
        }
    }
}