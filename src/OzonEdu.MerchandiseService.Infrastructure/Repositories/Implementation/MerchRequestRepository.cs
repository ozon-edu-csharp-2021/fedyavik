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
    public class MerchRequestRepository: IMerchRequestRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private const int Timeout = 5;

        public MerchRequestRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
            IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
        }
        
        public async Task<MerchRequest> CreateAsync(MerchRequest itemToCreate, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                INSERT INTO merch_requests (merch_item, employee)
                VALUES (@MerchItem, @EmployeeId);";
            var parameters = new
            {
                MerchItem = itemToCreate.MerchItem.Id.Value,
                EmployeeId = itemToCreate.Employee.Id.Value
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            await connection.ExecuteAsync(commandDefinition);
            _changeTracker.Track(itemToCreate);
            
            return itemToCreate;
        }

        public Task<MerchRequest> UpdateAsync(MerchRequest itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<MerchRequest> FindByEmployeeIdAsync(long id, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT employees.id, employees.first_name, employees.last_name, employees.second_name,
                       merch_types.id, merch_types.name, 
                       merch_items.id, merch_items.sku, merch_items.name, merch_items.merch_type,
                       request_statuses.id, request_statuses.name,
                       merch_requests.id, merch_requests.status, merch_requests.merch_item, merch_requests.employee
                FROM employees
                INNER JOIN merch_requests on merch_requests.employee = employees.id
                INNER JOIN request_statuses on request_statuses.id = merch_requests.status
                INNER JOIN merch_items on merch_items.id = merch_requests.merch_item
                INNER JOIN merch_types on merch_types.id = merch_items.merch_type
                WHERE employees.id = @EmployeeId";
            
            var parameters = new
            {
                EmployeeId = id,
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var merchRequests = await connection.QueryAsync<Models.Employee, Models.MerchType,
                Models.MerchItem, Models.MerchRequestStatus, Models.MerchRequest, MerchRequest>(commandDefinition,
                (employee, merchType, merchItem, requestStatus, merchRequest) => new MerchRequest(
                    new RequestNumber(merchRequest.Id),
                    new RequestStatus(requestStatus.Id, requestStatus.Name),
                    new MerchItem(new Id(merchItem.Id),  new Sku(merchItem.Sku), new Name(merchItem.Name), new MerchType(merchType.Id, merchType.Name)),
                    new Employee(new Id(employee.Id), new Fio(employee.First_Name, employee.Last_Name, employee.Second_Name))
                ));
            var merchRequest = merchRequests.FirstOrDefault();
            if (merchRequest is not null)
                _changeTracker.Track(merchRequest);
            
            return merchRequest;
        }

        public async Task<MerchRequest> FindByRequestNumberAsync(RequestNumber requestNumber, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT employees.id, employees.first_name, employees.last_name, employees.second_name,
                       merch_types.id, merch_types.name, 
                       merch_items.id, merch_items.sku, merch_items.name, merch_items.merch_type,
                       request_statuses.id, request_statuses.name,
                       merch_requests.id, merch_requests.status, merch_requests.merch_item, merch_requests.employee
                FROM merch_requests
                INNER JOIN employees on employees.id = merch_requests.employee
                INNER JOIN request_statuses on request_statuses.id = merch_requests.status
                INNER JOIN merch_items on merch_items.id = merch_requests.merch_item
                INNER JOIN merch_types on merch_types.id = merch_items.merch_type
                WHERE merch_requests.id = @Id";
            
            var parameters = new
            {
                Id = requestNumber.Value,
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var merchRequests = await connection.QueryAsync<Models.Employee, Models.MerchType,
                Models.MerchItem, Models.MerchRequestStatus, Models.MerchRequest, MerchRequest>(commandDefinition,
                (employee, merchType, merchItem, requestStatus, merchRequest) => new MerchRequest(
                    new RequestNumber(merchRequest.Id),
                    new RequestStatus(requestStatus.Id, requestStatus.Name),
                    new MerchItem(new Id(merchItem.Id),  new Sku(merchItem.Sku), new Name(merchItem.Name), new MerchType(merchType.Id, merchType.Name)),
                    new Employee(new Id(employee.Id), new Fio(employee.First_Name, employee.Last_Name, employee.Second_Name))
                ));
            var merchRequest = merchRequests.FirstOrDefault();
            if (merchRequest is not null)
                _changeTracker.Track(merchRequest);
            
            return merchRequest;
        }
    }
}