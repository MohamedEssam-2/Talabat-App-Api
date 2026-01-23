using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entitys.Basket;
using Shared.DTOS.BasketDTO;

namespace Services_Layer.MappingProfiles
{
    public class BasketMappingProfile : Profile
    {
        public BasketMappingProfile()
        {
            CreateMap<CustomerBasket,BasketDTO>().ReverseMap();

            CreateMap<BasketItem,BasketItemDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();


        }
    }
}
