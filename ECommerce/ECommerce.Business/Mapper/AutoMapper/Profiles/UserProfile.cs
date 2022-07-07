using AutoMapper;
using ECommerce.Business.Dtos.UserDtos;
using ECommerce.Entities.Concrete.Identity.Entities;

namespace ECommerce.Business.Mapper.AutoMapper.Profiles;

public class UserProfile:Profile
{
    public UserProfile()
    {
        CreateMap<AppUser, RegisterDto>().ReverseMap();
        CreateMap<AppUser, UserSummaryDto>().ReverseMap();
        CreateMap<AppUser, UserDto>().ReverseMap();
        CreateMap<AppUser, EditPasswordDto>().ReverseMap();
    }
}