using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.OrderModels;

namespace Services_Layer.Specifications
{
    internal class OrderWithPaymentIntentIdSpecification: BaseSpecification<Order,Guid>
    {
        public OrderWithPaymentIntentIdSpecification(string PaymentIntentId):base(o => o.PaymentIntentId == PaymentIntentId)
        {
            
        }
    }
}
