using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services_Abstraction;
using Shared.DTOS.BasketDTO;

namespace Presentation_Layer.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class Basket(IServiceManager serviceManager) :ControllerBase
    {
      
        [HttpGet]
        public async Task<ActionResult<BasketDTO>> GetBasket(string key)
        {
            var basket = await serviceManager.BasketService.GetBasketAsync(key);
            return Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult<BasketDTO>> CreateOrUpdateBasket(BasketDTO basket)
        {
            var createdOrUpdatedBasket = await serviceManager.BasketService.CreateOrUpdateBasketASunc(basket);
            return Ok(createdOrUpdatedBasket);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string key)
        {
            var result = await serviceManager.BasketService.DeleteBasketAsync(key);
                return Ok(result);
        }
    }
}
