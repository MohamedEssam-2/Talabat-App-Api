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
    public class OrderService(IMapper _mapper,IBasketRepository _basketRepository,IUnitOfWork _unitOfWork) : IOrderService
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
            //Get Delivery Method
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetByIdAsync(orderDto.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);

            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);

            var order = new Order(email, Address,deliveryMethod.Id,orderItems,subtotal);
            try
            {
                await _unitOfWork.GetRepository<Order, Guid>().AddAsync(order);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var message = ex.InnerException?.Message;
                var full = ex.ToString(); // ⬅️ ده هيطلع كل حاجة
                throw;
            }

            return _mapper.Map<OrderToReturn>(order);

        }
    }
}
