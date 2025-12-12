using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Entitys.Basket;
using StackExchange.Redis;

namespace Presistence_Layer.Repositories
{
    internal class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase database = connection.GetDatabase();
        public async Task<CustomerBasket?> CreateOrUpdate(CustomerBasket basket, TimeSpan? TimeTolive = null)
        {
           var jsonBasket = JsonSerializer.Serialize(basket);//convert c# object to Json
            var isCreatedOrUpdated =  await database.StringSetAsync(basket.Id, jsonBasket, TimeTolive ?? TimeSpan.FromDays(30));
            if (isCreatedOrUpdated)
                return basket;
            else
                return null;

        }

        public async Task<bool> DeleteBasketAsync(string key)
        {
           return await database.KeyDeleteAsync(key);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string key)
        {
            var basket = database.StringGet(key);
            if (basket.IsNullOrEmpty)
                return null;
            else
                return JsonSerializer.Deserialize<CustomerBasket>(basket!);//convert Json to c# object
        }
    }
}
