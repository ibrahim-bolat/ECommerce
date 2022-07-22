using System.Linq.Expressions;
using AutoMapper;
using ECommerce.Business.Abstract;
using ECommerce.Business.Constants;
using ECommerce.Business.Dtos.AddressDtos;
using ECommerce.DataAccess.Abstract;
using ECommerce.DataAccess.Concrete.EfCore.Contexts;
using ECommerce.Entities.Concrete;
using ECommerce.Entities.Concrete.Identity.Entities;
using ECommerce.Shared.Utilities.Abstract;
using ECommerce.Shared.Utilities.ComplexTypes;
using ECommerce.Shared.Utilities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Business.Concrete;

public class AddressManager:IAddressService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly DataContext _dataContext;

    public AddressManager(IUnitOfWork unitOfWork, IMapper mapper, DataContext dataContext)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _dataContext = dataContext;
    }

    public async Task<IResult> AddAsync(AddressDto addressDto, string createdByName)
    {
        var address = _mapper.Map<Address>(addressDto);
        address.CreatedByName = createdByName;
        address.ModifiedByName = createdByName;
        address.CreatedTime=DateTime.Now;
        address.ModifiedTime=DateTime.Now;
        address.IsActive = true;
        address.IsDeleted = false;
        var addedAddress = await _unitOfWork.AddressRepository.AddAsync(address);
        int result = await _unitOfWork.SaveAsync();
        if(result>0)
            return new DataResult<AddressDto>(ResultStatus.Success,Messages.AddressAdded, addressDto);
        return new Result(ResultStatus.Error,Messages.AddressNotAdded);
    }

    public async Task<IResult> UpdateAsync(AddressDto addressDto, string modifiedByName)
    {
        var address = await _unitOfWork.AddressRepository.GetAsync(x=>x.Id==addressDto.Id);
        if (address != null)
        {
            address = _mapper.Map<AddressDto,Address>(addressDto,address);
            address.ModifiedByName = modifiedByName;
            address.ModifiedTime = DateTime.Now;
            var updatedAddress = _unitOfWork.AddressRepository.UpdateAsync(address);
            var result = await _unitOfWork.SaveAsync();
            if(result>0)
                return new DataResult<AddressDto>(ResultStatus.Success,
                Messages.AddressUpdated, addressDto);
            return new Result(ResultStatus.Error,Messages.AddressNotUpdated);
        }
        return new Result(ResultStatus.Error,Messages.NotFound);
    }


    public async Task<IDataResult<AddressDto>> DeleteAsync(int id, string modifiedByName)
    {
        var address = await _unitOfWork.AddressRepository.GetAsync(x => x.Id == id);
        if (address != null)
        {
            address.IsActive = false;
            address.IsDeleted = true;
            address.ModifiedByName = modifiedByName;
            address.ModifiedTime = DateTime.Now;
            var deletedAddress = _unitOfWork.AddressRepository.UpdateAsync(address);
            var result = await _unitOfWork.SaveAsync();
            var addressDto  = _mapper.Map<AddressDto>(address);
            if(result>0)
                return new DataResult<AddressDto>(ResultStatus.Success, addressDto);
            return new DataResult<AddressDto>(ResultStatus.Error, Messages.AddressNotDeleted,null);
        }
        return new DataResult<AddressDto>(ResultStatus.Error, Messages.NotFound,null);
    }

    public async Task<IDataResult<AddressDto>> GetAsync(int id)
    {
        var address = await _unitOfWork.AddressRepository.GetAsync(x => x.Id == id && x.IsActive==true, x => x.AppUser);
        if (address != null)
        {
            var addressDto = _mapper.Map<AddressDto>(address);
            return new DataResult<AddressDto>(ResultStatus.Success, addressDto);
        }
        return new DataResult<AddressDto>(ResultStatus.Error, Messages.NotFound,null);
    }

    public async Task<IDataResult<IList<AddressDto>>> GetAllAsync()
    {
        var addresses = await _unitOfWork.AddressRepository.GetAllAsync(null, x => x.AppUser);
        var addressViewDtoList = _mapper.Map<IList<AddressDto>>(addresses);
        if (addresses.Count > -1)
        {
            return new DataResult<IList<AddressDto>>(ResultStatus.Success,addressViewDtoList);
        }
        return new DataResult<IList<AddressDto>>(ResultStatus.Error, Messages.NotFound,null);
    }
}