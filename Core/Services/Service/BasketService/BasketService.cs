using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entitys.Basket;
using Domain.Execptions;
using Services_Abstraction;
using Shared.DTOS.BasketDTO;


namespace Services_Layer.Service.BasketService
{
    internal class BasketService(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
    {
        public async Task<BasketDTO> CreateOrUpdateBasketASunc(BasketDTO basket)
        {
            var CustomerBasket = _mapper.Map<CustomerBasket>(basket);
            var CreatedOrUpdatedBasket = await _basketRepository.CreateOrUpdate(CustomerBasket);
            if (CreatedOrUpdatedBasket is not null)
                return await GetBasketAsync(basket.Id);
            else
                throw new Exception("Failed to create or update basket Try Again Later");
        }

        public async Task<bool> DeleteBasketAsync(string key)
        {
            return await _basketRepository.DeleteBasketAsync(key);
        }

        public async Task<BasketDTO> GetBasketAsync(string key)
        {
            var basket = await _basketRepository.GetBasketAsync(key);
            if (basket is not null) return _mapper.Map<BasketDTO>(basket);
            else
                throw new BasketNotFoundExceptions(key);



        }
    }
}
