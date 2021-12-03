namespace OzonEdu.MerchandiseService.Infrastructure.Repositories.Models
{
    public class MerchRequest
    {
        public long Id { get; set; }
        
        public string Status { get; set; }
        
        public int Merch_Item { get; set; }

        public int Employee { get; set; }
    }
}