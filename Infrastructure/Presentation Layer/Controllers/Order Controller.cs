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
    }
}
