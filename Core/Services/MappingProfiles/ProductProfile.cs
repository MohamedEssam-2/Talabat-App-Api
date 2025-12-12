using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entitys.Product;
using Shared.DTOS.Product;

namespace Services_Layer.MappingProfiles
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.ProductType.Name))
                //.ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => $"/*https://localhost:7267/{src.PictureUrl}" ));//Not good practice here
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<PictureUrlResolver>());
            CreateMap<ProductBrand, BrandDTO>();
            CreateMap<ProductType, TypeDTO>();
        }
    }
}
