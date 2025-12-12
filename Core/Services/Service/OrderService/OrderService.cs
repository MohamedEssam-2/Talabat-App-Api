using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entitys.Product;
using Domain.Execptions;
using DomainLayer.Models.OrderModels;
using Services_Abstraction;
using Shared.DTOS.OrderDTo;

namespace Services_Layer.Service.OrderService
{
    internal class OrderService(IMapper _mapper,IBasketRepository _basketRepository,IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderToReturn> CreateOrderAsync(OrderDto orderDto, string email)
        {
            // Map AddressDto To OrderAddress 
            var Address = _mapper.Map<OrderAddress>(orderDto.address);

            // Get Basket With Items
            var basket = await _basketRepository.GetBasketAsync(orderDto.BasketId)
                ?? throw new BasketNotFoundExceptions(orderDto.BasketId);

            List<OrderItem> orderItems = [];
            foreach (var item in basket.Items)
            {
               var originalProduct = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);
                var orderItem = new OrderItem()
                {
                  Product = new ProductItemOrdered()
                  {
                      ProductId = originalProduct.Id,
                      ProductName = originalProduct.Name,
                      PictureUrl = originalProduct.PictureUrl
                  },
                    Price = originalProduct.Price,
                    Quantity = item.Quantity
                };
                orderItems.Add(orderItem);

            }







            var order = new Order(email, Address);
        }
    }
}
