namespace OzonEdu.MerchandiseService.Models
{
    public class RequestMerchModel
    {
        public RequestMerchModel(long requestId, string status)
        {
            RequestId = requestId;
            Status = status;
        }

        public long RequestId { get; }
        public string Status { get; }
    }
}