namespace OzonEdu.MerchandiseService.Models
{
    public class RequestMerchModel
    {
        public RequestMerchModel(long requestId, string itemName)
        {
            RequestId = requestId;
            ItemName = itemName;
        }

        public long RequestId { get; }
        public string ItemName { get; }
    }
}