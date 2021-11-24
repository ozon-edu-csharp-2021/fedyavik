namespace OzonEdu.MerchandiseService.Infrastructure.Repositories.Models
{
    public class MerchItem
    {
        public long Id { get; set; }
        
        public long Sku { get; set; }
        
        public string Name { get; set; }
        
        public int Merch_Type { get; set; }
    }
}