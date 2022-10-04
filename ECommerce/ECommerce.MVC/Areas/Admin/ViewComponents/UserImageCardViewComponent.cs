using AutoMapper;
using ECommerce.Business.Abstract;
using ECommerce.Shared.Utilities.ComplexTypes;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MVC.Areas.Admin.ViewComponents;

[ViewComponent]
public class UserImageCardViewComponent : ViewComponent
    {
        private readonly IUserImageService _userImageService;

        public UserImageCardViewComponent(IUserImageService userImageService)
        {
            _userImageService = userImageService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int userId)
        {
            if (userId > 0)
            {
                var dresult = await _userImageService.GetAllByUserIdAsync(userId);
                if (dresult.ResultStatus==ResultStatus.Success)
                {
                    ViewBag.UserId = userId;
                    return View(dresult.Data);
                }
            }
            return View();
        }
    }
