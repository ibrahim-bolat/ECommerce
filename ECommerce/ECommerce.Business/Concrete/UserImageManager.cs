using AutoMapper;
using ECommerce.Business.Abstract;
using ECommerce.Business.Constants;
using ECommerce.Business.Dtos.UserImageDtos;
using ECommerce.DataAccess.Abstract;
using ECommerce.Entities.Concrete;
using ECommerce.Shared.Utilities.Abstract;
using ECommerce.Shared.Utilities.ComplexTypes;
using ECommerce.Shared.Utilities.Concrete;
using Microsoft.AspNetCore.Hosting;


namespace ECommerce.Business.Concrete;

public class UserImageManager:IUserImageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _hostEnvironment;

    public UserImageManager(IUnitOfWork unitOfWork, IMapper mapper,IWebHostEnvironment hostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _hostEnvironment = hostEnvironment;
    }

    public async Task<IResult> AddAsync(UserImageAddDto userImageAddDto, string createdByName)
    {
        var count = await _unitOfWork.UserImageRepository.CountAsync(x => x.UserId == userImageAddDto.UserId && x.IsActive);
        if (count >= 4)
        {
            return new Result(ResultStatus.Error, Messages.UserImageCountMoreThan4);
        }
        //Save image to wwwroot/admin/images/userimages/
        string wwwRootPath = _hostEnvironment.WebRootPath;
        string fileName = Path.GetFileNameWithoutExtension(userImageAddDto.ImageFile.FileName);
        string extension = Path.GetExtension(userImageAddDto.ImageFile.FileName);
        userImageAddDto.ImageTitle=fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
        string path = Path.Combine(wwwRootPath + "/admin/images/userimages/", fileName);
        using (var fileStream = new FileStream(path,FileMode.Create))
        {
            await userImageAddDto.ImageFile.CopyToAsync(fileStream);
        }
        UserImage userImage = _mapper.Map<UserImage>(userImageAddDto);
        userImage.ImagePath = Path.Combine( "/admin/images/userimages/", fileName);;
        userImage.CreatedByName = createdByName;
        userImage.ModifiedByName = createdByName;
        userImage.CreatedTime=DateTime.Now;
        userImage.ModifiedTime=DateTime.Now;
        userImage.IsActive = true;
        userImage.IsDeleted = false;
        var addedUserImage= await _unitOfWork.UserImageRepository.AddAsync(userImage);
        int result = await _unitOfWork.SaveAsync();
        if (result > 0)
            return new DataResult<UserImageAddDto>(ResultStatus.Success, Messages.UserImageAdded, userImageAddDto);
        return new Result(ResultStatus.Error, Messages.UserImageNotAdded);

    }

    public async Task<IResult> UpdateAsync(UserImageDto userImageDto, string modifiedByName)
    {
        var userImage = _mapper.Map<UserImage>(userImageDto);
        userImage.ModifiedByName = modifiedByName;
        userImage.ModifiedTime = DateTime.Now;
        var updatedUserImage= await _unitOfWork.UserImageRepository.UpdateAsync(userImage);
        var result = await _unitOfWork.SaveAsync();
        if (result > 0)
            return new DataResult<UserImageDto>(ResultStatus.Success,
                Messages.UserImageUpdated, userImageDto);
        return new Result(ResultStatus.Error, Messages.UserImageNotUpdated);
    }
    public async Task<IDataResult<UserImageDto>> DeleteAsync(int id, string modifiedByName)
    {
        var userImage = await _unitOfWork.UserImageRepository.GetAsync(x => x.Id == id && x.IsActive==true);
        if (userImage != null)
        {
            userImage.IsActive = false;
            userImage.IsDeleted = true;
            userImage.ModifiedByName = modifiedByName;
            userImage.ModifiedTime = DateTime.Now;
            var deletedUserImage = _unitOfWork.UserImageRepository.UpdateAsync(userImage);
            var result = await _unitOfWork.SaveAsync();
            var userImageDto = _mapper.Map<UserImageDto>(userImage);
            if (result > 0)
                return new DataResult<UserImageDto>(ResultStatus.Success, userImageDto);
            return new DataResult<UserImageDto>(ResultStatus.Error, Messages.UserImageNotDeleted, null);
        }
        return new DataResult<UserImageDto>(ResultStatus.Error, Messages.NotFound, null);
    }

    public async Task<IDataResult<UserImageDto>> GetAsync(int id)
    {
        var userImage = await _unitOfWork.UserImageRepository.GetAsync(x => x.Id == id && x.IsActive, x => x.AppUser);
        var userImageViewDto = _mapper.Map<UserImageDto>(userImage);
        if (userImage != null)
        {
            return new DataResult<UserImageDto>(ResultStatus.Success,userImageViewDto);
        }
        return new DataResult<UserImageDto>(ResultStatus.Error, Messages.NotFound,userImageViewDto);
    }
    
    public async Task<IDataResult<UserImageDto>> GetProfilImageByUserIdAsync(int userId)
    {
        var userImage = await _unitOfWork.UserImageRepository.GetAsync(x => x.UserId == userId && x.IsActive && x.Profil, x => x.AppUser);
        var userImageViewDto = _mapper.Map<UserImageDto>(userImage);
        if (userImage != null)
        {
            return new DataResult<UserImageDto>(ResultStatus.Success,userImageViewDto);
        }
        return new DataResult<UserImageDto>(ResultStatus.Error, Messages.NotFound,userImageViewDto);
    }
    
    public async Task<IDataResult<int>> GetUserImageCountByUserIdAsync(int userId)
    {
        var count = await _unitOfWork.UserImageRepository.CountAsync(x => x.UserId == userId && x.IsActive);
        return new DataResult<int>(ResultStatus.Success, count);
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
    
    public async Task<IDataResult<IList<UserImageDto>>> GetAllByUserIdAsync(int userId)
    {
        var userImages = await _unitOfWork.UserImageRepository.GetAllAsync(ui=>ui.UserId==userId && ui.IsActive , x => x.AppUser);
        var userImagesViewDtoList = _mapper.Map<IList<UserImageDto>>(userImages);
        if (userImages.Count > -1)
        {
            return new DataResult<IList<UserImageDto>>(ResultStatus.Success,userImagesViewDtoList);
        }
        return new DataResult<IList<UserImageDto>>(ResultStatus.Error, Messages.NotFound,null);
    }

    public async Task<IResult> SetProfilImageAsync(int id,int userId,string modifiedByName)
    {
        var userImages = await _unitOfWork.UserImageRepository.GetAllAsync(ui=>ui.UserId==userId && ui.IsActive , x => x.AppUser);
        if (userImages !=null)
        {
            foreach (var userImage in userImages)
            {
                if (userImage.Id != id)
                {
                    userImage.Profil = false;
                    userImage.ModifiedByName = modifiedByName;
                    userImage.ModifiedTime = DateTime.Now;
                    await _unitOfWork.UserImageRepository.UpdateAsync(userImage);
                }
                else
                {
                    if (userImage.Profil == false)
                    {
                        userImage.Profil = true;
                        userImage.ModifiedByName = modifiedByName;
                        userImage.ModifiedTime = DateTime.Now;
                        await _unitOfWork.UserImageRepository.UpdateAsync(userImage);
                    }
                }
            }
            var result = await _unitOfWork.SaveAsync();
            if (result > 0)
                return new Result(ResultStatus.Success, Messages.UserImageSetProfil);
        }
        return new Result(ResultStatus.Success, Messages.UserImageSetProfil);
    }
}