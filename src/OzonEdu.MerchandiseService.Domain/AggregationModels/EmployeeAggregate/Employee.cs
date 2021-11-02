using System.Dynamic;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate
{
    public class Employee: Entity
    {
        public Employee(Id id, Fio fio)
        {
            Id = id;
            Fio = fio;
        }

        public Id Id { get; }
        public Fio Fio { get; private set; }

        public void ChangeFio(Fio newFio)
        {
            Fio = newFio;
        }
    }
}