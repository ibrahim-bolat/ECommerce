using ECommerce.Business.Dtos.UserDtos;
using ECommerce.Shared.Utilities.Abstract;

namespace ECommerce.Business.Abstract;

public interface IUserService
{
    Task<IDataResult<UserDetailDto>> GetWithAddressAsync(int id);
}