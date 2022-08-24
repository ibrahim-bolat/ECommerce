using AutoMapper;
using ECommerce.Business.Abstract;
using ECommerce.Business.Constants;
using ECommerce.Business.Dtos.AddressDtos;
using ECommerce.Business.Dtos.UserImageDtos;
using ECommerce.Entities.Concrete;
using ECommerce.Entities.Concrete.Identity.Entities;
using ECommerce.Shared.Utilities.ComplexTypes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ECommerce.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class UserImageController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IUserImageService _userImageService;
    private readonly IMapper _mapper;

    public UserImageController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        RoleManager<AppRole> roleManager, IUserImageService userImageService, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _userImageService = userImageService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult UserImageAdd(int userId)
    {
        UserImageAddDto userImageAddDto = new UserImageAddDto();
        userImageAddDto.UserId = userId;
        return View(userImageAddDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> UserImageAdd(UserImageAddDto userImageAddDto)
    {
        if (ModelState.IsValid)
        {
            var dresult= await _userImageService.AddAsync(userImageAddDto, User.Identity?.Name);
            if (dresult.Message == Messages.UserImageCountMoreThan4)
            {
                ModelState.AddModelError("UserImageCountMoreThan4", Messages.UserImageCountMoreThan4);
               return View(userImageAddDto);
            }
            if (dresult.ResultStatus == ResultStatus.Success)
            {
                TempData["AddUserImageSuccess"] = true;
                return RedirectToAction("UserImageAdd", "UserImage" ,new { area = "Admin" ,userId=userImageAddDto.UserId});
            }
        }
        return View(userImageAddDto);
    }
    [HttpPost]
    public async Task<IActionResult> UserImageSetProfil(int id,int userId)
    {
        if (id>0)
        {
            var dresult= await _userImageService.SetProfilImageAsync(id,userId, User.Identity?.Name);
            if (dresult.ResultStatus==ResultStatus.Success)
            {
                return Json(new { success = true});
            }
            return Json(new { success = false});
        }
        return RedirectToAction("AllErrorPages", "ErrorPages", new { statusCode = 404 });
    }
    
        
    [HttpPost]
    public async Task<IActionResult> UserImageDelete(int id)
    {
        if (id > 0)
        {
            var dresult= await _userImageService.DeleteAsync(id, User.Identity?.Name);
            if (dresult.ResultStatus==ResultStatus.Success)
            {
                return Json(new { success = true});
            }
            return Json(new { success = false});
        }
        return RedirectToAction("AllErrorPages", "ErrorPages", new { statusCode = 404 });
    }
}