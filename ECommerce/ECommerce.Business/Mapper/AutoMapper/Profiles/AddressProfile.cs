using AutoMapper;
using ECommerce.Business.Dtos.AddressDtos;
using ECommerce.Entities.Concrete;

namespace ECommerce.Business.Mapper.AutoMapper.Profiles;

public class AddressProfile:Profile
{
    public AddressProfile()
    {
        CreateMap<Address, AddressDto>().ReverseMap();
        CreateMap<Address, AddressSummaryDto>().ReverseMap();
    }
}