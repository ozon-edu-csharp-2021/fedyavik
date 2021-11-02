using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchItemAggregate;
using Xunit;

namespace OzonEdu.MerchandiseService.Domain.Tests
{
    public class EmployeeTests
    {
        [Fact]
        public void CreateEmployeeSuccess()
        {
            //Arrange
            var id = 321321;
            var firstName = "name";
            var lastName = "Lname";
            var secondName = "Sname";
            
            //Act
            var employee = new Employee(
                new Id(id),
                new Fio(firstName, lastName, secondName)
                );
            
            //Assert
            Assert.Equal(id, employee.Id.Value);
            Assert.Equal(firstName, employee.Fio.FirstName);
            Assert.Equal(lastName, employee.Fio.LastName);
            Assert.Equal(secondName, employee.Fio.SecondName);
            Assert.Equal($"{firstName} {lastName} {secondName}", employee.Fio.FullName);
        }
        
        [Fact]
        public void ChangeEmployeeLastNameSuccess()
        {
            //Arrange
            var id = 321321;
            var firstName = "name";
            var lastName = "Lname";
            var secondName = "Sname";
            
            var employee = new Employee(
                new Id(id),
                new Fio(firstName, lastName, secondName)
            );
            
            //Act
            var newLastName = "newLastName";
            Fio newFio = new Fio(firstName, newLastName, secondName);
            employee.ChangeFio(newFio);
            
            //Assert
            Assert.Equal(id, employee.Id.Value);
            Assert.Equal(firstName, employee.Fio.FirstName);
            Assert.Equal(newLastName, employee.Fio.LastName);
            Assert.Equal(secondName, employee.Fio.SecondName);
            Assert.Equal($"{firstName} {newLastName} {secondName}", employee.Fio.FullName);
        }
    }
}