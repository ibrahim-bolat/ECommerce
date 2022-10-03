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
            ViewBag.ProfilPhoto = "/admin/images/avatar/unspecifieduseravatar.png";
            if (appUser.UserImages !=null && appUser.UserImages.Count > 0)
            {
                foreach (var userImage in appUser.UserImages)
                {
                    if (userImage.Profil && userImage.IsActive)
                        ViewBag.ProfilPhoto = userImage.ImagePath;
                }
            }
            return View();
        }
    }
