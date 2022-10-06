using AutoMapper;
using ECommerce.Business.Abstract;
using ECommerce.Business.Constants;
using ECommerce.Business.Dtos.AddressDtos;
using ECommerce.Business.Dtos.UserDtos;
using ECommerce.DataAccess.Abstract;
using ECommerce.Shared.Utilities.Abstract;
using ECommerce.Shared.Utilities.ComplexTypes;
using ECommerce.Shared.Utilities.Concrete;

namespace ECommerce.Business.Concrete;

public class UserManager:IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<IDataResult<UserDetailDto>> GetWithAddressAsync(int id)
    {
        var user = await _unitOfWork.UserRepository.GetAsync(x => x.Id == id && x.IsActive==true, x => x.Addresses);
        if (user != null)
        {
            if (user.IsActive)
            {
                UserDto userDto = _mapper.Map<UserDto>(user);
                List<AddressSummaryDto> addressSummaryDtos =
                    _mapper.Map<List<AddressSummaryDto>>(user.Addresses.Where(a => a.IsActive));
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
}