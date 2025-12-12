using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entitys.IdentityModels;
using Shared.DTOS.IdentityDto;

namespace Services_Layer.MappingProfiles
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
