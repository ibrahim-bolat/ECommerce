using AutoMapper;
using ECommerce.Business.Abstract;
using ECommerce.Business.Constants;
using ECommerce.Business.Dtos.UserImageDtos;
using ECommerce.DataAccess.Abstract;
using ECommerce.Entities.Concrete;
using ECommerce.Shared.Utilities.Abstract;
using ECommerce.Shared.Utilities.ComplexTypes;
using ECommerce.Shared.Utilities.Concrete;

namespace ECommerce.Business.Concrete;

public class UserImageManager:IUserImageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserImageManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IDataResult<UserImageDto>> AddAsync(UserImageDto userImageDto, string createdByName)
    {
        var userImage = _mapper.Map<UserImage>(userImageDto);
        userImage.CreatedByName = createdByName;
        userImage.ModifiedByName = createdByName;
        userImage.CreatedTime=DateTime.Now;
        userImage.ModifiedTime=DateTime.Now;
        userImage.IsActive = true;
        userImage.IsDeleted = false;
        var addedUserImage= await _unitOfWork.UserImageRepository.AddAsync(userImage);
        await _unitOfWork.SaveAsync();
        return new DataResult<UserImageDto>(ResultStatus.Success,
            Messages.ImageAdded, userImageDto);

    }

    public async Task<IDataResult<UserImageDto>> UpdateAsync(UserImageDto userImageDto, string modifiedByName)
    {
        var userImage = _mapper.Map<UserImage>(userImageDto);
        userImage.ModifiedByName = modifiedByName;
        userImage.ModifiedTime = DateTime.Now;
        var updatedUserImage= await _unitOfWork.UserImageRepository.UpdateAsync(userImage);
        await _unitOfWork.SaveAsync();
        return new DataResult<UserImageDto>(ResultStatus.Success,
            Messages.ImageUpdated,userImageDto);
    }


    public async Task<IResult> DeleteAsync(int id, string modifiedByName)
    {
        var userImage = await _unitOfWork.UserImageRepository.GetAsync(x => x.Id == id);
        if (userImage != null)
        {
            userImage.IsActive = false;
            userImage.IsDeleted = true;
            userImage.ModifiedByName = modifiedByName;
            userImage.ModifiedTime = DateTime.Now;
            await _unitOfWork.UserImageRepository.UpdateAsync(userImage);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, Messages.ImageDeleted);
        }
        return new Result(ResultStatus.Error, Messages.NotFound);
    }

    public async Task<IDataResult<UserImageDto>> GetAsync(int id)
    {
        var userImage = await _unitOfWork.UserImageRepository.GetAsync(x => x.Id == id, x => x.AppUser);
        var userImageViewDto = _mapper.Map<UserImageDto>(userImage);
        if (userImage != null)
        {
            return new DataResult<UserImageDto>(ResultStatus.Success,userImageViewDto);
        }

        return new DataResult<UserImageDto>(ResultStatus.Error, Messages.NotFound,userImageViewDto);
    }

    public async Task<IDataResult<IList<UserImageDto>>> GetAllAsync()
    {
        var userImages = await _unitOfWork.UserImageRepository.GetAllAsync(null, x => x.AppUser);
        var userImagesViewDtoList = _mapper.Map<IList<UserImageDto>>(userImages);
        if (userImages.Count > -1)
        {
            return new DataResult<IList<UserImageDto>>(ResultStatus.Success,userImagesViewDtoList);
        }
        return new DataResult<IList<UserImageDto>>(ResultStatus.Error, Messages.NotFound,null);
    }
}