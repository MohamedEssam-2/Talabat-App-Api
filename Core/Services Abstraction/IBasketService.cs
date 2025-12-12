using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTOS.BasketDTO;

namespace Services_Abstraction
{
    public interface IBasketService
    {
        Task<BasketDTO> GetBasketAsync(string key);
        Task<BasketDTO> CreateOrUpdateBasketASunc(BasketDTO basket);
        Task<bool> DeleteBasketAsync(string key);
    }
}
