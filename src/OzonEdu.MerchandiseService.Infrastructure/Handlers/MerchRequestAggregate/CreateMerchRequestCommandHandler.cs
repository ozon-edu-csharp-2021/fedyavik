using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Domain.Contracts;
using OzonEdu.MerchandiseService.Infrastructure.Commands.CreateMerchRequest;

namespace OzonEdu.MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate
{
    public class CreateMerchRequestCommandHandler: IRequestHandler<CreateMerchRequestCommand, long>
    {
        public readonly IMerchRequestRepository _merchRequestRepository;
        public readonly IEmployeeRepository _employeeRepository;
        public readonly IMerchItemRepository _merchItemRepository;

        public CreateMerchRequestCommandHandler(IMerchRequestRepository merchRepository,
            IEmployeeRepository employeeRepository,
            IMerchItemRepository merchItemRepository)
        {
            _merchRequestRepository = merchRepository;
            _employeeRepository = employeeRepository;
            _merchItemRepository = merchItemRepository;
        }

        public async Task<long> Handle(CreateMerchRequestCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.FindByIdAsync(request.EmployeeId, cancellationToken);
            if (employee is null)
                throw new Exception($"Employee not found by id {request.EmployeeId}");
            
            var merchRequest = await _merchRequestRepository.FindByEmployeeIdAsync(employee.Id.Value, cancellationToken);
            if (merchRequest is not null)
                throw new Exception($"Merch request with user {employee.Fio.FullName} already exist");

            var merchItem = await _merchItemRepository.FindBySkuAsync(request.Sku, cancellationToken);
            if (merchItem is null)
                throw new Exception($"Merch item with sku {request.Sku} not found");
            
            var newMerchRequest = new MerchRequest(
                null,
                RequestStatus.InWork,
                merchItem,
                employee
            );

            var result = await _merchRequestRepository.CreateAsync(newMerchRequest, cancellationToken);
            //await _merchRequestRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return result.RequestNumber.Value;
        }
    }
}