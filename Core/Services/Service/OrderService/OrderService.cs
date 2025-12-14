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
using Services_Layer.Specifications;
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
                var full = ex.ToString();
                throw;
            }

            return _mapper.Map<OrderToReturn>(order);

        }

        public async Task<IEnumerable<OrderToReturn>> GetAllOrderAsync(string email)
        {
            var spec = new OrderSpecification(email);
            var order = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(spec);
            return _mapper.Map<IEnumerable<OrderToReturn>>(order);
        }

        public async Task<IEnumerable<DeliveryMrthodDto>> GetDeliveryMethods()
        {
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMrthodDto>>(deliveryMethod);
        }

        public async Task<OrderToReturn> GetOrderById(Guid id)
        {
            var spec = new OrderSpecification(id);
            var order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(spec);
            return _mapper.Map<OrderToReturn>(order);
        }
    }
}
