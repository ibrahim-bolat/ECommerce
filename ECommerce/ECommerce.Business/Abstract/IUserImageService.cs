using ECommerce.Business.Dtos.UserImageDtos;
using ECommerce.Entities.Concrete;
using ECommerce.Shared.Utilities.Abstract;

namespace ECommerce.Business.Abstract;

public interface IUserImageService
{
    Task<IResult> AddAsync(UserImageAddDto userImageAddDto, string createdByName);
    Task<IResult> UpdateAsync(UserImageDto userImageDto, string modifiedByName);
    Task<IDataResult<UserImageDto>> DeleteAsync(int id, string modifiedByName);
    Task<IDataResult<UserImageDto>> GetAsync(int id);
    Task<IDataResult<UserImageDto>> GetProfilImageByUserIdAsync(int userId);
    Task<IDataResult<int>> GetUserImageCountByUserIdAsync(int userId);
    Task<IDataResult<IList<UserImageDto>>> GetAllAsync();
    Task<IDataResult<IList<UserImageDto>>> GetAllByUserIdAsync(int userId);
    Task<IResult> SetProfilImageAsync(int id,int userId,string modifiedByName);
}