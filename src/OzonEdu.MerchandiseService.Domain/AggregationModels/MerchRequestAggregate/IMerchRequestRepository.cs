using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.Contracts;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate
{
    public interface IMerchRequestRepository: IRepository<MerchRequest>
    {
        /// <summary>
        /// Получить заявку по идентификатору пользователя
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Объект заявки</returns>
        Task<MerchRequest> FindByEmployeeIdAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получить заявку по номеру заявки
        /// </summary>
        /// <param name="requestNumber">Номер заявки</param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Объект заявки</returns>
        Task<MerchRequest> FindByRequestNumberAsync(RequestNumber requestNumber,
            CancellationToken cancellationToken = default);
    }
}