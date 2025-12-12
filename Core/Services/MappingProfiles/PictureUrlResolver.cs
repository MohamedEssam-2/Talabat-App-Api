
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entitys.Product;
using Microsoft.Extensions.Configuration;
using Shared.DTOS.Product;

namespace Services_Layer.MappingProfiles
{
    public class PictureUrlResolver(IConfiguration _configuration) : IValueResolver<Product, ProductDTO, string>
    {
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
            {
                return string.Empty;
            }
            else
            {
                var url =  $"{_configuration.GetSection("Url")["BaseUrl"]}{source.PictureUrl}";
                return url;

            }
        }
    }
}
