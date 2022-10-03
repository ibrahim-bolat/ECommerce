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
using Microsoft.AspNetCore.Http;
using IResult = ECommerce.Shared.Utilities.Abstract.IResult;


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
        UserImage userImage = _mapper.Map<UserImage>(userImageAddDto);
        userImage.ImagePath = await UploadImage(userImageAddDto); //Resmi kaydet wwwroot/admin/images/userimages/
        userImage.CreatedByName = createdByName;
        userImage.ModifiedByName = createdByName;
        userImage.CreatedTime=DateTime.Now;
        userImage.ModifiedTime=DateTime.Now;
        userImage.IsActive = true;
        userImage.IsDeleted = false;
        var addedUserImage= await _unitOfWork.UserImageRepository.AddAsync(userImage);
        if (count > 0 && count < 4 && userImage.Profil)
        {
            var userImages =
                await _unitOfWork.UserImageRepository.GetAllAsync(ui =>
                    ui.UserId == userImageAddDto.UserId && ui.IsActive);
            if (userImages != null)
            {
                foreach (var uImage in userImages)
                {
                    if (uImage.Profil)
                    {
                        uImage.Profil = false;
                        uImage.ModifiedByName = createdByName;
                        uImage.ModifiedTime = DateTime.Now;
                        await _unitOfWork.UserImageRepository.UpdateAsync(uImage);
                    }
                }
            }
        }
        int result = await _unitOfWork.SaveAsync();
        if (result > 0)
            return new DataResult<UserImageAddDto>(ResultStatus.Success, Messages.UserImageAdded, userImageAddDto);
        return new Result(ResultStatus.Error, Messages.UserImageNotAdded);

    }

    private async Task<string> UploadImage(UserImageAddDto userImageAddDto)
    {
        var imageFileName = Path.GetFileNameWithoutExtension(userImageAddDto.ImageFile.FileName);

        var localImageFileDir = $"wwwroot/admin/images/userimages";// wwwroot/admin/images/userimages
        var extension = Path.GetExtension(userImageAddDto.ImageFile.FileName).ToLower();

        //local userimages klasörü yoksa oluştur.
        if (!Directory.Exists(Path.Combine(localImageFileDir)))
        {
            Directory.CreateDirectory(Path.Combine(localImageFileDir));
        }

        var localImageFilePath = $"{localImageFileDir}/{imageFileName}{extension}"; // wwwroot/admin/images/userimages/profil.jpg

        int count = 1;
        var tempFileName = imageFileName;
        while (File.Exists(localImageFilePath))
        {
            imageFileName = string.Format("{0}{1}", tempFileName, count++);
            localImageFilePath = $"{localImageFileDir}/{imageFileName}{extension}";
        }

        var path = $"/admin/images/userimages/{imageFileName}{extension}";// /admin/images/userimages/profil.jpg

        using (Stream fileStream = new FileStream(localImageFilePath, FileMode.Create))
        {
            await userImageAddDto.ImageFile.CopyToAsync(fileStream);
        }

        return path;
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
            var imagePath = _hostEnvironment.WebRootPath + userImage.ImagePath;
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
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
        }
        return new DataResult<UserImageDto>(ResultStatus.Error, Messages.NotFound, null);
    }

    public async Task<IDataResult<UserImageDto>> GetAsync(int id)
    {
        var userImage = await _unitOfWork.UserImageRepository.GetAsync(x => x.Id == id && x.IsActive);
        var userImageViewDto = _mapper.Map<UserImageDto>(userImage);
        if (userImage != null)
        {
            return new DataResult<UserImageDto>(ResultStatus.Success,userImageViewDto);
        }
        return new DataResult<UserImageDto>(ResultStatus.Error, Messages.NotFound,userImageViewDto);
    }
    
    public async Task<IDataResult<UserImageDto>> GetProfilImageByUserIdAsync(int userId)
    {
        var userImage = await _unitOfWork.UserImageRepository.GetAsync(x => x.UserId == userId && x.IsActive && x.Profil);
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
        var userImages = await _unitOfWork.UserImageRepository.GetAllAsync();
        var userImagesViewDtoList = _mapper.Map<IList<UserImageDto>>(userImages);
        if (userImages.Count > -1)
        {
            return new DataResult<IList<UserImageDto>>(ResultStatus.Success,userImagesViewDtoList);
        }
        return new DataResult<IList<UserImageDto>>(ResultStatus.Error, Messages.NotFound,null);
    }
    
    public async Task<IDataResult<IList<UserImageDto>>> GetAllByUserIdAsync(int userId)
    {
        var userImages = await _unitOfWork.UserImageRepository.GetAllAsync(ui=>ui.UserId==userId && ui.IsActive);
        var userImagesViewDtoList = _mapper.Map<IList<UserImageDto>>(userImages);
        if (userImages.Count > -1)
        {
            return new DataResult<IList<UserImageDto>>(ResultStatus.Success,userImagesViewDtoList);
        }
        return new DataResult<IList<UserImageDto>>(ResultStatus.Error, Messages.NotFound,null);
    }

    public async Task<IResult> SetProfilImageAsync(int id,int userId,string modifiedByName)
    {
        var userImages = await _unitOfWork.UserImageRepository.GetAllAsync(ui=>ui.UserId==userId && ui.IsActive);
        if (userImages !=null)
        {
            foreach (var userImage in userImages)
            {
                if (userImage.Id != id && userImage.Profil )
                {
                    userImage.Profil = false;
                    userImage.ModifiedByName = modifiedByName;
                    userImage.ModifiedTime = DateTime.Now;
                    await _unitOfWork.UserImageRepository.UpdateAsync(userImage);
                }
                else
                {
                    if (userImage.Id == id && userImage.Profil==false )
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