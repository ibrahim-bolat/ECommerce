using ECommerce.Entities.Concrete.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MVC.Areas.Admin.ViewComponents;

[ViewComponent]
public class AdminHeaderViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public AdminHeaderViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public  IViewComponentResult Invoke()
        {
            AppUser user =  _userManager.FindByNameAsync(User.Identity?.Name).Result;
            return View(user);
        }
    }
