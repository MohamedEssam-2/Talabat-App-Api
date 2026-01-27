using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services_Abstraction;

namespace Presentation_Layer.Controllers
{
    public class PaymentsController (IServiceManager serviceManager) :ApiBaseController
    {
        [Authorize]
        [HttpPost  ("{basketId}")]
        public async Task<IActionResult> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await serviceManager.PaymentService.CreateOrUpdatePaymentIntent(basketId);
            //if (basket == null) return BadRequest(new ProblemDetails { Title = "Problem with your basket" });
            return Ok(basket);
        }
    }
}
