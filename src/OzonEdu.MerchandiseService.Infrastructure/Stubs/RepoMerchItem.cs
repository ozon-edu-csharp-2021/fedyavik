using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchItemAggregate;
using OzonEdu.MerchandiseService.Domain.Contracts;

namespace OzonEdu.MerchandiseService.Infrastructure.Stubs
{
    public class RepoMerchItem : IMerchItemRepository
    {
        private List<MerchItem> _merchItems = new List<MerchItem>()
        {
            new MerchItem( new Id(1),  new Sku(0), new Name("Peeeen"), MerchType.Pen),
            new MerchItem(new Id(2), new Sku(1), new Name("Padpad"), MerchType.Notepad),
        };
        public IUnitOfWork UnitOfWork { get; }
        public Task<MerchItem> CreateAsync(MerchItem itemToCreate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<MerchItem> UpdateAsync(MerchItem itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<MerchItem> FindBySkuAsync(long sku, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_merchItems.FirstOrDefault(item => item.Sku.Value == sku));
        }
    }
}