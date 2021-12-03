using System;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchItemAggregate;
using Xunit;

namespace OzonEdu.MerchandiseService.Domain.Tests
{
    public class MerchItemTests
    {
        [Fact]
        public void CreateMerchItemSuccess()
        {
            //Arrange
            var sku = 12333;
            var name = "Big pen";
            var merchType = MerchType.Pen;
            
            //Act
            var merchItem = new MerchItem(
                new Id(0), 
                new Sku(sku), 
                new Name(name), 
                merchType);
            
            //Assert
            Assert.Equal(sku, merchItem.Sku.Value);
            Assert.Equal(name, merchItem.Name.Value);
            Assert.Equal(merchType, merchItem.MerchType);
        }

        [Fact]
        public void ChangeMerchItemNameSuccess()
        {
            //Arrange
            var sku = 12333;
            var name = "Big pen";
            var merchType = MerchType.Pen;
            
            var merchItem = new MerchItem(
                new Id(0), 
                new Sku(sku), 
                new Name(name), 
                merchType);
            
            
            //Act
            string newName = "new name";
            merchItem.ChangeName(new Name(newName));
            
            //Assert
            Assert.Equal(newName, merchItem.Name.Value);
        }
        
        [Fact]
        public void ChangeMerchItemNameFailure()
        {
            //Arrange
            var sku = 12333;
            var name = "Big pen";
            var merchType = MerchType.Pen;
            
            var merchItem = new MerchItem(
                new Id(0), 
                new Sku(sku), 
                new Name(name), 
                merchType);

            //Act
            Name newName = new Name("");
            
            
            //Assert
            Assert.Throws<Exception>(() => merchItem.ChangeName(newName));
        }
    }
}