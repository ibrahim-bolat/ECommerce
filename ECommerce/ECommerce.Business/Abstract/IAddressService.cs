using ECommerce.Business.Dtos.AddressDtos;
using ECommerce.Entities.Concrete;
using ECommerce.Shared.Utilities.Abstract;

namespace ECommerce.Business.Abstract;

public interface IAddressService
{
    Task<IDataResult<AddressDto>> AddAsync(AddressDto userAddressDto, string createdByName);
    Task<IDataResult<AddressDto>> UpdateAsync(AddressDto articleUpdateDto, string modifiedByName);
    Task<IResult> DeleteAsync(int id, string modifiedByName);
    Task<IDataResult<AddressDto>> GetAsync(int articleId);
    Task<IDataResult<IList<AddressDto>>> GetAllAsync();
}