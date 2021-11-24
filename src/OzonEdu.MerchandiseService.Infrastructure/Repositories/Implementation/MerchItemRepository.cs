using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchItemAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Domain.Contracts;
using OzonEdu.MerchandiseService.Infrastructure.Repositories.Infrastructure.Interfaces;

namespace OzonEdu.MerchandiseService.Infrastructure.Repositories.Implementation
{
    public class MerchItemRepository: IMerchItemRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private const int Timeout = 5;

        public MerchItemRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
            IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
        }
        
        public Task<MerchItem> CreateAsync(MerchItem itemToCreate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<MerchItem> UpdateAsync(MerchItem itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<MerchItem> FindBySkuAsync(long sku, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT merch_items.id, merch_items.sku, merch_items.name, merch_items.merch_type,
                       merch_types.id, merch_types.name
                from merch_items
                INNER JOIN merch_types on merch_types.id = merch_items.merch_type
                where sku = @Sku";
            var parameters = new
            {
                Sku = sku
            };
            
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);

            var merchItems = await connection.QueryAsync<Models.MerchItem, Models.MerchType, MerchItem>(commandDefinition,
                (merchItem, merchType) => new MerchItem(
                    new Id(merchItem.Id), 
                    new Sku(merchItem.Sku),
                    new Name(merchItem.Name),
                    new MerchType(merchType.Id, merchType.Name)
                    ));
            var merchItem = merchItems.First();
            if (merchItem is not null)
                _changeTracker.Track(merchItem);
            return merchItem;
        }
    }
}