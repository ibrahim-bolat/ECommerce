using AutoMapper;
using ECommerce.Business.Abstract;
using ECommerce.Business.Dtos.UserDtos;
using ECommerce.Entities.Concrete.Identity.Entities;
using ECommerce.Shared.Utilities.ComplexTypes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MVC.Areas.Admin.ViewComponents;

[ViewComponent]
public class UserSummaryCardViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserImageService _userImageService;
        private readonly IMapper _mapper;

        public UserSummaryCardViewComponent(UserManager<AppUser> userManager, IUserImageService userImageService, IMapper mapper)
        {
            _userManager = userManager;
            _userImageService = userImageService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId.ToString());
            var countResult= await _userImageService.GetUserImageCountByUserIdAsync(userId);
            if (countResult.ResultStatus == ResultStatus.Success)
                ViewBag.UserImageCount = countResult.Data;

            var profilResult = await _userImageService.GetProfilImageByUserIdAsync(userId);
            if (profilResult.ResultStatus == ResultStatus.Success)
            {
                if (profilResult.Data != null)
                    ViewBag.UserImageProfilImagePath = profilResult.Data.ImagePath;
                
                if (countResult.Data > 0)
                {
                    var dresult = await _userImageService.GetAllByUserIdAsync(userId);
                    if (dresult.ResultStatus==ResultStatus.Success)
                    {
                        ViewBag.UserImageList = dresult.Data;
                    }
                }
            }
            else 
            {
                    if (countResult.Data > 0)
                    {
                        var dresult = await _userImageService.GetAllByUserIdAsync(userId);
                        if (dresult.ResultStatus==ResultStatus.Success)
                        {
                            ViewBag.UserImageList = dresult.Data;
                        }
                    }
           }
            UserCardSummaryDto userCardSummaryDto = _mapper.Map<UserCardSummaryDto>(user);
            return View(userCardSummaryDto);
        }
    }
