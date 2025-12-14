using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTOS.OrderDTo;

namespace Services_Abstraction
{
    public interface IOrderService
    {
        public Task<OrderToReturn> CreateOrderAsync(OrderDto orderDto, string email);

        public Task <IEnumerable<DeliveryMrthodDto>> GetDeliveryMethods();

        public Task<OrderToReturn> GetOrderById(Guid id );

        public Task<IEnumerable<OrderToReturn>> GetAllOrderAsync(string email);

    }
}
