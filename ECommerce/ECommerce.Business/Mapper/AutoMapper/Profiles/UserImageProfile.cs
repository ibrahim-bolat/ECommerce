using AutoMapper;
using ECommerce.Business.Dtos.UserImageDtos;
using ECommerce.Entities.Concrete;

namespace ECommerce.Business.Mapper.AutoMapper.Profiles;

public class UserImageProfile:Profile
{
    public UserImageProfile()
    {
        CreateMap<UserImage, UserImageDto>().ReverseMap();
        CreateMap<UserImage, UserImageAddDto>().ForMember(dest => dest.ImageFile,
            src => src.Ignore()).ReverseMap();
        CreateMap<UserImageDto, UserImageAddDto>().ForMember(dest => dest.ImageFile,
            src => src.Ignore()).ReverseMap();
    }
}