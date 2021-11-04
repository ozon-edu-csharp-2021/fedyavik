using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Domain.Contracts;

namespace OzonEdu.MerchandiseService.Infrastructure.Stubs
{
    public class RepoMerchRequest : IMerchRequestRepository
    {
        public static List<MerchRequest> _merchRequests = new List<MerchRequest>();
        public IUnitOfWork UnitOfWork { get; }
        public Task<MerchRequest> CreateAsync(MerchRequest itemToCreate, CancellationToken cancellationToken = default)
        {
            int reqNumber = _merchRequests.Count > 0 ? _merchRequests.Max(req => req.Id) + 1 : 1;
                
            itemToCreate.SetRequestNumber(reqNumber);
            _merchRequests.Add(itemToCreate);
            return Task.FromResult(itemToCreate);
        }

        public Task<MerchRequest> UpdateAsync(MerchRequest itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<MerchRequest> FindByEmployeeIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_merchRequests.FirstOrDefault(req => req.Employee.Id.Value == id));
        }

        public Task<MerchRequest> FindByRequestNumberAsync(RequestNumber requestNumber, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_merchRequests.FirstOrDefault(req => req.RequestNumber.Value == requestNumber.Value));
        }
    }
}