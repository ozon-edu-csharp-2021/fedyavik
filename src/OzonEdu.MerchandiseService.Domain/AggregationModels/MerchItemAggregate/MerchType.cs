using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchItemAggregate
{
    public class MerchType: Enumeration
    {
        public static MerchType Notepad = new MerchType(1, nameof(Notepad));
        public static MerchType Pen = new MerchType(1, nameof(Pen));
        public MerchType(int id, string name) : base(id, name)
        {
        }
    }
}