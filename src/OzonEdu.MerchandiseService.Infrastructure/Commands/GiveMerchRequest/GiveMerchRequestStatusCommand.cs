using MediatR;

namespace OzonEdu.MerchandiseService.Infrastructure.Commands.GiveMerchRequest
{
    public class GiveMerchRequestStatusCommand: IRequest<string>
    {
        /// <summary>
        /// Идентификатор запроса
        /// </summary>
        public long RequestId { get; set; }
    }
}