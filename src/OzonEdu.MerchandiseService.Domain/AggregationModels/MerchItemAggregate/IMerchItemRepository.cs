using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchItemAggregate;

namespace OzonEdu.MerchandiseService.Domain.Contracts
{
    public interface IMerchItemRepository: IRepository<MerchItem>
    {
        /// <summary>
        /// Найти товарную позицию по складскому идентфикатору
        /// </summary>
        /// <param name="sku">Идентификатор пользователя</param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Объект пользователя</returns>
        Task<MerchItem> FindBySkuAsync(long sku, CancellationToken cancellationToken = default);
    }
}