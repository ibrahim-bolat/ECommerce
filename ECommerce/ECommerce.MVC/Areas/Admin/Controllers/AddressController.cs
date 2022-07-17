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
                var currentUser = await _userManager.FindByNameAsync(User.Identity?.Name);
                AppUser user = await _userManager.FindByIdAsync(userId);
                Address updatedAddress = user.Addresses.Where(a => a.Id == addressDto.Id).FirstOrDefault();
                if (user != null)
                {
                    if (updatedAddress != null)
                    {
                        user.Addresses.ForEach(x =>
                        {
                            if (x.Id == addressDto.Id)
                            {
                                x.AddressTitle=addressDto.AddressTitle;
                                x.AddressType=addressDto.AddressType;
                                x.Street=addressDto.Street;
                                x.MainStreet=addressDto.MainStreet;
                                x.NeighborhoodOrVillage=addressDto.NeighborhoodOrVillage;
                                x.District=addressDto.District;
                                x.City=addressDto.City;
                                x.Country=addressDto.Country;
                                x.RegionOrState= addressDto.RegionOrState;
                                x.BuildingNo = addressDto.BuildingNo;
                                x.FlatNo = addressDto.FlatNo;
                                x.PostalCode = addressDto.PostalCode;
                                x.AddressDetails = addressDto.AddressDetails;
                                x.DefaultAddress = addressDto.DefaultAddress;
                                x.Note = addressDto.Note;
                                x.ModifiedByName = currentUser.UserName;
                                x.ModifiedTime = DateTime.Now;
                            }
                        });
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

    [HttpGet]
    public IActionResult DetailAddress(int addressId)
    {
        var dresult = _addressService.GetAsync(addressId);
        if (dresult.ResultSatus != ResultStatus.Error)
        {
            return View(dresult.Data);
        }
        return RedirectToAction("AllErrorPages", "ErrorPages", new { statusCode = 404 });
    }
}