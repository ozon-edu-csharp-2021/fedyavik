using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate
{
    public class Fio: ValueObject
    {
        public Fio(string firstName,
            string lastName,
            string secondName)
        {
            FirstName = firstName;
            LastName = lastName;
            SecondName = secondName;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public string SecondName { get; }

        public string FullName => $"{FirstName} {LastName} {SecondName}";
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FullName;
        }
    }
}