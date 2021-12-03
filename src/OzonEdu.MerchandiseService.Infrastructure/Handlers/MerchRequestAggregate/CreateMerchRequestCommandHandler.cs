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
        private readonly IMerchRequestRepository _merchRequestRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMerchItemRepository _merchItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        
        public CreateMerchRequestCommandHandler(IMerchRequestRepository merchRepository,
            IEmployeeRepository employeeRepository,
            IMerchItemRepository merchItemRepository,
            IUnitOfWork unitOfWork)
        {
            _merchRequestRepository = merchRepository;
            _employeeRepository = employeeRepository;
            _merchItemRepository = merchItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<long> Handle(CreateMerchRequestCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.StartTransaction(cancellationToken);
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

            var createResult = await _merchRequestRepository.CreateAsync(newMerchRequest, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return createResult.Id;
        }
    }
}