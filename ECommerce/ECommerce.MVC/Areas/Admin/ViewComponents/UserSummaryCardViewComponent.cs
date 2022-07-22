using AutoMapper;
using ECommerce.Business.Dtos.UserDtos;
using ECommerce.Entities.Concrete.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MVC.Areas.Admin.ViewComponents;

[ViewComponent]
public class UserSummaryCardViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserSummaryCardViewComponent(UserManager<AppUser> userManager,IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity?.Name);
            UserCardSummaryDto userCardSummaryDto = _mapper.Map<UserCardSummaryDto>(user);
            return View(userCardSummaryDto);
        }
    }
