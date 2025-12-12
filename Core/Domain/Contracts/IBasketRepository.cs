using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entitys.Basket;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        Task <CustomerBasket?>GetBasketAsync(string key);
        Task<CustomerBasket?> CreateOrUpdate(CustomerBasket basket,TimeSpan? timeSpan=null);
        Task<bool> DeleteBasketAsync(string key);
    }
}
