using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entitys.IdentityModels;
using DomainLayer.Models.OrderModels;
using Shared.DTOS.IdentityDto;
using Shared.DTOS.OrderDTo;

namespace Services_Layer.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDto,OrderAddress>().ReverseMap();

            CreateMap<Order, OrderToReturn>()
                .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName)).ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<OrderPictureUrlResolver>());
        }
    }
}
