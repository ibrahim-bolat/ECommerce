using ECommerce.Business.Dtos.UserImageDtos;
using ECommerce.Entities.Concrete;
using ECommerce.Shared.Utilities.Abstract;

namespace ECommerce.Business.Abstract;

public interface IUserImageService
{
    Task<IDataResult<UserImageDto>> AddAsync(UserImageDto userAddressDto, string createdByName);
    Task<IDataResult<UserImageDto>> UpdateAsync(UserImageDto articleUpdateDto, string modifiedByName);
    Task<IResult> DeleteAsync(int id, string modifiedByName);
    Task<IDataResult<UserImageDto>> GetAsync(int articleId);
    Task<IDataResult<IList<UserImageDto>>> GetAllAsync();
}