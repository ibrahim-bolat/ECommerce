using AutoMapper;
using ECommerce.Business.Abstract;
using ECommerce.Business.Dtos.AddressDtos;
using ECommerce.Entities.Concrete;
using ECommerce.Entities.Concrete.Identity.Entities;
using ECommerce.Shared.Utilities.ComplexTypes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ECommerce.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class AddressController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IAddressService _addressService;
    private readonly IMapper _mapper;

    public AddressController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        RoleManager<AppRole> roleManager, IAddressService addressService, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _addressService = addressService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult AddAddress(int userId)
    {
        TempData["userId"] = userId.ToString();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddAddress(AddressDto addressDto)
    {
        if (ModelState.IsValid)
        {
            var userId = TempData["userId"]?.ToString();
            IdentityResult result = null;
            if (userId != null)
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                AppUser user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    Address address = _mapper.Map<Address>(addressDto);
                    address.CreatedByName = currentUser.UserName;
                    address.ModifiedByName = currentUser.UserName;
                    address.CreatedTime=DateTime.Now;
                    address.ModifiedTime=DateTime.Now;
                    address.IsActive = true;
                    address.IsDeleted = false;
                    user.Addresses.Add(address);
                    result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                        return View(addressDto);
                    }
                    result = await _userManager.UpdateSecurityStampAsync(user);
                    if (!result.Succeeded)
                    {
                        result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                        return View(addressDto);
                    }
                    TempData["AddAddressSuccess"] = true;
                    return RedirectToAction("AddAddress", "Address" ,new { userId = userId});
                }
            }
            return RedirectToAction("AllErrorPages", "ErrorPages" ,new { statusCode = 404});
        }
        return View();
    }
    
        [HttpGet]
    public async Task<IActionResult>  UpdateAddress(int userId,int addressId)
    {
        TempData["userId"] = userId.ToString();
        AppUser user = await _userManager.FindByIdAsync(userId.ToString());
        if (user != null)
        {
            Address address = user.Addresses.Where(a => a.Id == addressId).FirstOrDefault();
            if (address != null)
            {
                AddressDto addressDto = _mapper.Map<AddressDto>(address);
                return View(addressDto);
            }
        }
        return RedirectToAction("AllErrorPages", "ErrorPages" ,new { statusCode = 404});
    }

    [HttpPost]
    public async Task<IActionResult> UpdateAddress(AddressDto addressDto)
    {
        if (ModelState.IsValid)
        {
            var userId = TempData["userId"]?.ToString();
            IdentityResult result = null;
            if (userId != null)
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                AppUser user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    for (int i = 0; i < user.Addresses.Count; i++)
                    {
                        if (user.Addresses[i].Id == addressDto.Id)
                        {
                            user.Addresses[i] = _mapper.Map<AddressDto,Address>(addressDto);
                            user.Addresses[i].ModifiedByName = currentUser.UserName;
                            user.Addresses[i].ModifiedTime=DateTime.Now;
                            break;
                        }
                    }
                    result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                        return View(addressDto);
                    }
                    result = await _userManager.UpdateSecurityStampAsync(user);
                    if (!result.Succeeded)
                    {
                        result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                        return View(addressDto);
                    }
                    TempData["UpdateAddressSuccess"] = true;
                    return RedirectToAction("UpdateAddress", "Address" ,new { userId = userId,addressId=addressDto.Id});
                }
            }
            return RedirectToAction("AllErrorPages", "ErrorPages" ,new { statusCode = 404});
        }
        return View();
    }
}