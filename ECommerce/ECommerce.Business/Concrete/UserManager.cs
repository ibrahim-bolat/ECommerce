using System.Linq.Expressions;
using AutoMapper;
using ECommerce.Business.Abstract;
using ECommerce.Business.Constants;
using ECommerce.Business.Dtos.AddressDtos;
using ECommerce.Business.Dtos.UserDtos;
using ECommerce.DataAccess.Abstract;
using ECommerce.DataAccess.Concrete.EfCore.Contexts;
using ECommerce.Entities.Concrete;
using ECommerce.Entities.Concrete.Identity.Entities;
using ECommerce.Shared.Utilities.Abstract;
using ECommerce.Shared.Utilities.ComplexTypes;
using ECommerce.Shared.Utilities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Business.Concrete;

public class UserManager:IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly DataContext _dataContext;

    public UserManager(IUnitOfWork unitOfWork, IMapper mapper, DataContext dataContext)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _dataContext = dataContext;
    }

    public async Task<IResult> AddAsync(UserDto userDto, string createdByName)
    {
        var user = _mapper.Map<AppUser>(userDto);
        user.CreatedByName = createdByName;
        user.ModifiedByName = createdByName;
        user.CreatedTime=DateTime.Now;
        user.ModifiedTime=DateTime.Now;
        user.IsActive = true;
        user.IsDeleted = false;
        var addedUser = await _unitOfWork.UserRepository.AddAsync(user);
        int result = await _unitOfWork.SaveAsync();
        if(result>0)
            return new DataResult<UserDto>(ResultStatus.Success,Messages.UserAdded, userDto);
        return new Result(ResultStatus.Error,Messages.UserNotAdded);
    }
    
    public async Task<IResult> UpdateAsync(UserDto userDto, string modifiedByName)
    {
        var user = await _unitOfWork.UserRepository.GetAsync(x=>x.Id==userDto.Id);
        if (user != null)
        {
            user = _mapper.Map<UserDto,AppUser>(userDto,user);
            user.ModifiedByName = modifiedByName;
            user.ModifiedTime = DateTime.Now;
            var updatedUser = _unitOfWork.UserRepository.UpdateAsync(user);
            var result = await _unitOfWork.SaveAsync();
            if(result>0)
                return new DataResult<UserDto>(ResultStatus.Success,
                    Messages.UserUpdated, userDto);
            return new Result(ResultStatus.Error,Messages.UserNotUpdated);
        }
        return new Result(ResultStatus.Error,Messages.NotFound);
    }
    
    public async Task<IDataResult<UserDto>> DeleteAsync(int id, string modifiedByName)
    {
        var user = await _unitOfWork.UserRepository.GetAsync(x => x.Id == id);
        if (user != null)
        {
            user.IsActive = false;
            user.IsDeleted = true;
            user.ModifiedByName = modifiedByName;
            user.ModifiedTime = DateTime.Now;
            var deletedAddress = _unitOfWork.UserRepository.UpdateAsync(user);
            var result = await _unitOfWork.SaveAsync();
            var userDto  = _mapper.Map<UserDto>(user);
            if(result>0)
                return new DataResult<UserDto>(ResultStatus.Success, userDto);
            return new DataResult<UserDto>(ResultStatus.Error, Messages.UserNotDeleted,null);
        }
        return new DataResult<UserDto>(ResultStatus.Error, Messages.NotFound,null);
    }
    
    public async Task<IDataResult<UserDetailDto>> GetAsync(int id)
    {
        var user = await _unitOfWork.UserRepository.GetAsync(x => x.Id == id && x.IsActive==true, x => x.Addresses);
        if (user != null)
        {
            if (user.IsActive)
            {
                UserDto userDto = _mapper.Map<UserDto>(user);
                List<AddressSummaryDto> addressSummaryDtos =
                    _mapper.Map<List<AddressSummaryDto>>(user.Addresses.Where(a => a.IsActive == true));
                UserDetailDto userDetailDto = new UserDetailDto()
                {
                    UserDto = userDto,
                    UserAddressSummaryDtos = addressSummaryDtos
                };
                return new DataResult<UserDetailDto>(ResultStatus.Success, userDetailDto);
            }
        }
        return new DataResult<UserDetailDto>(ResultStatus.Error, Messages.NotFound,null);
    }
    
    public async Task<IDataResult<IList<UserDto>>> GetAllAsync()
    {
        var users = await _unitOfWork.UserRepository.GetAllAsync(null, x => x.Addresses);
        var userDtoList = _mapper.Map<IList<UserDto>>(users);
        if (users.Count > -1)
        {
            return new DataResult<IList<UserDto>>(ResultStatus.Success,userDtoList);
        }
        return new DataResult<IList<UserDto>>(ResultStatus.Error, Messages.NotFound,null);
    }
    
}