using ECommerce.Entities.Concrete.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MVC.Areas.Admin.ViewComponents;

[ViewComponent]
public class AdminHeaderAvatarViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public AdminHeaderAvatarViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            AppUser appUser =  await _userManager.FindByNameAsync(User.Identity?.Name);
            ViewBag.UserId = appUser.Id;
            return View();
        }
    }
