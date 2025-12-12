using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using DomainLayer.Models.OrderModels;
using Services_Abstraction;
using Shared.DTOS.OrderDTo;

namespace Services_Layer.Service.OrderService
{
    internal class OrderService(IMapper _mapper,IBasketRepository _basketRepository) : IOrderService
    {
        public Task<OrderToReturn> CreateOrderAsync(OrderDto orderDto, string email)
        {
            // Map AddressDto To OrderAddress 
            var Address = _mapper.Map<OrderAddress>(orderDto.address);
            



            var order = new Order(email, Address,);
        }
    }
}
