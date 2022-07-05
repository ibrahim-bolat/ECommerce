using AutoMapper;
using ECommerce.Business.Dtos.RoleDtos;
using ECommerce.Entities.Concrete.Identity.Entities;

namespace ECommerce.Business.Mapper.AutoMapper.Profiles;

public class RoleProfile:Profile
{
    public RoleProfile()
    {
        CreateMap<AppRole, RoleDto>().ReverseMap();
    }
}