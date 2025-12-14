using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services_Abstraction;
using Shared.DTOS.OrderDTo;

namespace Presentation_Layer.Controllers
{
    public class OrderController(IServiceManager _serviceManager) : ApiBaseController
    {
        [Authorize]
        [HttpPost]
        public async Task<ActionResult <OrderToReturn>> CreateOrder(OrderDto orderDto)
        {

         var orderToReturn = await _serviceManager.OrderService.CreateOrderAsync(orderDto, GetEmailFromToken());
            return Ok(orderToReturn);
        }
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMrthodDto>>> GetDeliveryMethods()
        {
            var deliveryMethods = await _serviceManager.OrderService.GetDeliveryMethods();
            return Ok(deliveryMethods);
        }
        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturn>> GetOrderById(Guid id)
        {
            var order = await _serviceManager.OrderService.GetOrderById(id);
            return Ok(order);
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturn>>> GetAllOrderAsync()
        {
            var orders = await _serviceManager.OrderService.GetAllOrderAsync(GetEmailFromToken());
            return Ok(orders);
        }
    }
}
