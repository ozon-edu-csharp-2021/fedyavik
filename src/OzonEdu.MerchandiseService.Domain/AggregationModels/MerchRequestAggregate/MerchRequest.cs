using System;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchItemAggregate;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate
{
    public class MerchRequest: Entity
    {
        public MerchRequest(
            RequestNumber requestNumber,
            RequestStatus requestStatus,
            MerchItem merchItem,
            Employee employee)
        {
            RequestNumber = requestNumber;
            RequestStatus = requestStatus;
            MerchItem = merchItem;
            Employee = employee;
        }

        public RequestNumber RequestNumber { get; private set; }
        public RequestStatus RequestStatus { get; private set; }
        public MerchItem MerchItem { get; }
        public Employee Employee { get; }
        
        /// <summary>
        /// Смена статуса у заявки
        /// </summary>
        /// <param name="status"></param>
        /// <exception cref="Exception"></exception>
        public void ChangeStatus(RequestStatus status)
        {
            if (RequestStatus.Equals(RequestStatus.Done))
                throw new Exception($"Request in done. Change status unavailable");
            
            RequestStatus = status;
        }
        
        /// <summary>
        /// Задание номена заявки
        /// </summary>
        /// <param name="number"></param>
        /// <exception cref="Exception"></exception>
        public void SetRequestNumber(long number)
        {
            RequestNumber = new RequestNumber(number);
        }
    }
}