using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.OrderModels;

namespace Services_Layer.Specifications
{
    internal class OrderSpecification:BaseSpecification<Order,Guid>
    {
   

        public OrderSpecification(Guid id) : base(o => o.Id == id)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Items);

        }
    }
}
