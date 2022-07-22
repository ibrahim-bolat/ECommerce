using AutoMapper;
using ECommerce.Business.Dtos.AddressDtos;
using ECommerce.Entities.Concrete;

namespace ECommerce.Business.Mapper.AutoMapper.Profiles;

public class AddressProfile:Profile
{
    public AddressProfile()
    {
        CreateMap<Address, AddressDto>().ReverseMap();
        CreateMap<Address, AddressSummaryDto>() 
            .ForMember(dest => dest.FullName
                , opt => opt.MapFrom(src => src.FirstName+" "+src.LastName))
            .ReverseMap();

    }
}