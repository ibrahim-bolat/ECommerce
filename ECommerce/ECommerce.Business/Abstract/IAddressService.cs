using ECommerce.Business.Dtos.AddressDtos;
using ECommerce.Entities.Concrete;
using ECommerce.Shared.Utilities.Abstract;

namespace ECommerce.Business.Abstract;

public interface IAddressService
{
    Task<IResult> AddAsync(AddressDto addressDto, string createdByName);
    Task<IResult> UpdateAsync(AddressDto addressDto, string modifiedByName);
    Task<IDataResult<AddressDto>> DeleteAsync(int id, string modifiedByName);
    Task<IDataResult<AddressDto>> GetAsync(int id);
    Task<IDataResult<IList<AddressDto>>> GetAllAsync();
}