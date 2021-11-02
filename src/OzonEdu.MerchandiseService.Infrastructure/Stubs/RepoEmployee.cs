using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.Contracts;

namespace OzonEdu.MerchandiseService.Infrastructure.Stubs
{
    public class RepoEmployee : IEmployeeRepository
    {
        private List<Employee>  _employees = new List<Employee>()
        {
            new Employee(new Id(0), new Fio("F", "I", "O")),
            new Employee(new Id(1), new Fio("Ff", "Ii", "Oo")),
            new Employee(new Id(2), new Fio("Fff", "Iii", "Ooo")),
        };
        
        public IUnitOfWork UnitOfWork { get; }
        public Task<Employee> CreateAsync(Employee itemToCreate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> UpdateAsync(Employee itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_employees.FirstOrDefault(emp => emp.Id.Value == id));
        }
    }
}