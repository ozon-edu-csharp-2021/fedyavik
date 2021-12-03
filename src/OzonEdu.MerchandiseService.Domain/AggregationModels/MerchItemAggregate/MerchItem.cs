using System;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchItemAggregate
{
    public class MerchItem: Entity
    {
        public MerchItem(
            Id id,
            Sku sku,
            Name name,
            MerchType merchType)
        {
            Id = id;
            Name = name;
            Sku = sku;
            MerchType = merchType;
        }
        public Id Id { get; }
        public Name Name { get; private set; }
        public MerchType MerchType { get; }
        public Sku Sku { get; }

        public void ChangeName(Name newName)
        {
            if (newName.Value.Length == 0)
                throw new Exception($"value is empty");
            Name = newName;
        }
    }
}