using System;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchItemAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using Xunit;

namespace OzonEdu.MerchandiseService.Domain.Tests
{
    public class MerchRequestTests
    {
        [Fact]
        public void CreateMerchRequestSuccess()
        {
            //Arrange
            var requestNumber = 11111;
            
            var sku = 12333;
            var name = "Big pen";
            var merchType = MerchType.Pen;
            var merchItem = new MerchItem(
                new Sku(sku), 
                new Name(name), 
                merchType);
            
            var id = 321321;
            var firstName = "name";
            var lastName = "Lname";
            var secondName = "Sname";
            
            var employee = new Employee(
                new Id(id),
                new Fio(firstName, lastName, secondName)
            );
            
            //Act
            var merchRequest = new MerchRequest(
                new RequestNumber(requestNumber),
                RequestStatus.InWork,
                merchItem,
                employee
            );
            
            //Assert
            Assert.Equal(requestNumber, merchRequest.RequestNumber.Value);
            Assert.Equal(RequestStatus.InWork, merchRequest.RequestStatus);
        }

        [Fact]
        public void ChangeMerchRequestStatusSuccess()
        {
            //Arrange
            var requestNumber = 11111;
            
            var sku = 12333;
            var name = "Big pen";
            var merchType = MerchType.Pen;
            var merchItem = new MerchItem(
                new Sku(sku), 
                new Name(name), 
                merchType);
            
            var id = 321321;
            var firstName = "name";
            var lastName = "Lname";
            var secondName = "Sname";
            
            var employee = new Employee(
                new Id(id),
                new Fio(firstName, lastName, secondName)
            );
            
            var merchRequest = new MerchRequest(
                new RequestNumber(requestNumber),
                RequestStatus.InWork,
                merchItem,
                employee
            );
            
            
            //Act
            merchRequest.ChangeStatus(RequestStatus.Done);
            
            //Assert
            Assert.Equal(RequestStatus.Done, merchRequest.RequestStatus);
        }
        
        [Fact]
        public void ChangeMerchRequestStatusFailure()
        {
            //Arrange
            var requestNumber = 11111;
            
            var sku = 12333;
            var name = "Big pen";
            var merchType = MerchType.Pen;
            var merchItem = new MerchItem(
                new Sku(sku), 
                new Name(name), 
                merchType);
            
            var id = 321321;
            var firstName = "name";
            var lastName = "Lname";
            var secondName = "Sname";
            
            var employee = new Employee(
                new Id(id),
                new Fio(firstName, lastName, secondName)
            );
            
            var merchRequest = new MerchRequest(
                new RequestNumber(requestNumber),
                RequestStatus.Done,
                merchItem,
                employee
            );
            
            
            //Act
            
            //Assert
            Assert.Throws<Exception>(() => merchRequest.ChangeStatus(RequestStatus.InWork));
        }
        [Fact]
        public void ChangeMerchRequestNumberSuccess()
        {
            //Arrange

            var sku = 12333;
            var name = "Big pen";
            var merchType = MerchType.Pen;
            var merchItem = new MerchItem(
                new Sku(sku), 
                new Name(name), 
                merchType);
            
            var id = 321321;
            var firstName = "name";
            var lastName = "Lname";
            var secondName = "Sname";
            
            var employee = new Employee(
                new Id(id),
                new Fio(firstName, lastName, secondName)
            );
            
            var merchRequest = new MerchRequest(
                null,
                RequestStatus.Done,
                merchItem,
                employee
            );
            
            
            //Act
            var requestNumber = 22222;
            merchRequest.SetRequestNumber(requestNumber);
            
            //Assert
            Assert.Equal(requestNumber, merchRequest.RequestNumber.Value);
        }
    }
}