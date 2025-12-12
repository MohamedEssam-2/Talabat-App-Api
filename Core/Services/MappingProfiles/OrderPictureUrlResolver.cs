using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Models.OrderModels;
using Microsoft.Extensions.Configuration;
using Shared.DTOS.OrderDTo;

namespace Services_Layer.MappingProfiles
{
    public class OrderPictureUrlResolver(IConfiguration _configuration) : IValueResolver<OrderItem, OrderItemDto, string>
    {
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureUrl))
            {
                return string.Empty;
            }
            else
            {
                var url = $"{_configuration.GetSection("Url")["BaseUrl"]}{source.Product.PictureUrl}";
                return url;

            }
        }
    }
}
