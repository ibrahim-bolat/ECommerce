using ECommerce.Business.Dtos.AddressDtos;
using ECommerce.Business.Dtos.UserDtos;
using ECommerce.Entities.Concrete;
using ECommerce.Shared.Utilities.Abstract;

namespace ECommerce.Business.Abstract;

public interface IUserService
{
    Task<IResult> AddAsync(UserDto userDto, string createdByName);
    Task<IResult> UpdateAsync(UserDto userDto, string modifiedByName);
    Task<IDataResult<UserDto>> DeleteAsync(int id, string modifiedByName);
    Task<IDataResult<UserDetailDto>> GetAsync(int id);
    Task<IDataResult<IList<UserDto>>> GetAllAsync();
}