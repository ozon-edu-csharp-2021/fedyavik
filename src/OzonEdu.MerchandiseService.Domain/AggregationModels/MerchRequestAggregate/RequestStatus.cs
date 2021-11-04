using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate
{
    public class RequestStatus: Enumeration
    {
        public static RequestStatus InWork = new RequestStatus(1, "InWork");
        public static RequestStatus Done = new RequestStatus(2, "Done");
        public RequestStatus(int id, string name) : base(id, name)
        {
        }
    }
}