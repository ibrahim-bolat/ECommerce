using AutoMapper;
using ECommerce.Business.Dtos.UserDtos;
using ECommerce.Entities.Concrete.Identity.Entities;

namespace ECommerce.Business.Mapper.AutoMapper.Profiles;

public class UserProfile:Profile
{
    public UserProfile()
    {
        CreateMap<AppUser, RegisterDto>().ReverseMap();
        CreateMap<AppUser, UserAllDetailsDto>().ReverseMap();
        CreateMap<AppUser, UserBriefDetailsDto>().ReverseMap();
        CreateMap<AppUser, UserDto>().ReverseMap();
    }
}