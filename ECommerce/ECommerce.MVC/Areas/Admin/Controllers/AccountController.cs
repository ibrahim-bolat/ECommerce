using System.Web;
using ECommerce.Entities.Concrete.Identity.Entities;
using ECommerce.Entities.Enums;
using ECommerce.Helpers.MailHelper;
using ECommerce.MVC.Areas.Admin.Models.ViewModels.Account;
using ECommerce.Shared.Service.Abtract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace ECommerce.MVC.Areas.Admin.Controllers;

[Area("Admin")]
public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IEmailService _emailService;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        RoleManager<AppRole> roleManager,IEmailService emailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _emailService = emailService;
    }

    [AllowAnonymous]
    [HttpGet] 
    public IActionResult Register()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost] 
    public async Task<IActionResult> Register(RegisterViewModel Model)
    {
        if (ModelState.IsValid)
        {
            AppUser applicationUser = new AppUser
            {
                FirstName = Model.FirstName,
                LastName = Model.LastName,
                UserName = Model.UserName,
                Email = Model.Email
            };
            AppRole role = await _roleManager.FindByNameAsync("User");
            if (role == null)
                await _roleManager.CreateAsync(new AppRole { Name = "User" });
            IdentityResult userResult = await _userManager.CreateAsync(applicationUser, Model.Password);
            IdentityResult roleResult = null;
            if (userResult.Succeeded)
            {
                roleResult = await _userManager.AddToRoleAsync(applicationUser, "User");
                if (roleResult.Succeeded)
                {
                    TempData["LoginSuccess"] = true;
                    return RedirectToAction("Login", "Account", new { area = "Admin" });
                }
                roleResult.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                return View(Model);
            }
            userResult.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
            return View(Model);
        }
        return View(Model);
    }

    [AllowAnonymous]
    [HttpGet("[action]/{email}/{token}")] 
    public async Task<IActionResult> ConfirmEmail(string email, string token)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            ViewBag.State = false;
            return View("ConfirmEmail");
        }
        var result = await _userManager.ConfirmEmailAsync(user, HttpUtility.UrlDecode(token));
        if (result.Succeeded)
        {
            ViewBag.State = true;
            return View("ConfirmEmail");
        }
        return View();
    }

    [AllowAnonymous]
    [HttpGet] 
    public IActionResult Login(string ReturnUrl = "Index")
    {
        TempData["returnUrl"] = ReturnUrl;
        return View();
    }

    [AllowAnonymous]
    [HttpPost] 
    public async Task<IActionResult> Login(LoginViewModel Model)
    {
        if (ModelState.IsValid)
        {
            AppUser user = await _userManager.FindByEmailAsync(Model.Email);
            if (user != null)
            {
                await _signInManager.SignOutAsync();
                SignInResult result =
                    await _signInManager.PasswordSignInAsync(user, Model.Password, Model.Persistent, Model.Lock);

                if (result.Succeeded)
                {
                    await _userManager.ResetAccessFailedCountAsync(user);
                    if (string.IsNullOrEmpty(TempData["returnUrl"] != null ? TempData["returnUrl"].ToString() : ""))
                        return RedirectToAction("Index", "Home");
                    if (TempData["returnUrl"].Equals("Index") || TempData["returnUrl"].Equals("/"))
                        return RedirectToAction("Index", "Home");
                    
                    return Redirect(TempData["returnUrl"].ToString());
                }
                else
                {
                    await _userManager.AccessFailedAsync(user);
                    int failcount = await _userManager.GetAccessFailedCountAsync(user);
                    if (failcount == 3)
                    {
                        await _userManager.SetLockoutEndDateAsync(user,
                            new DateTimeOffset(DateTime.Now
                                .AddMinutes(30))); 
                        ModelState.AddModelError("Locked",
                            "Art arda 3 başarısız giriş denemesi yaptığınızdan dolayı hesabınız 30 dk kilitlenmiştir.");
                    }
                    else
                    {
                        if (result.IsLockedOut)
                        {
                            ModelState.AddModelError("Locked",
                                "Art arda 3 başarısız giriş denemesi yaptığınızdan dolayı hesabınız 30 dk kilitlenmiştir.");
                            return View(Model);
                        }
                        ModelState.AddModelError("InvalidEpostaOrPass1", "E-posta veya şifre yanlış.");
                        return View(Model);
                    }
                }
            }
            ModelState.AddModelError("NoUser", "Böyle bir kullanıcı bulunmamaktadır.");
            ModelState.AddModelError("InvalidEpostaOrPass2", "E-posta veya şifre yanlış.");
            return View(Model);
        }
        return View(Model);
    }

    [HttpGet] 
    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }

    [AllowAnonymous]
    [HttpGet] 
    public IActionResult ForgetPass()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost] 
    public async Task<IActionResult> ForgetPass(ForgetPassViewModel model)
    {
        AppUser user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var confirmationLink = Url.Action("UpdatePassword", "Account",
                new { area = "Admin", userId = user.Id, token = HttpUtility.UrlEncode(token) }, Request.Scheme);

            MailRequest mailRequest = new MailRequest
            {
                ToMail = model.Email,
                ConfirmationLink = confirmationLink,
                MailSubject = "Şifre Güncelleme Talebi",
                IsBodyHtml = true,
                MailLinkTitle = "Yeni şifre talebi için tıklayınız"
            };
            bool emailResponse = _emailService.SendEmail(mailRequest);
            if (emailResponse)
            {
                ViewBag.State = true;
            }
            else
            {
                ViewBag.State = false;
            }
            ViewBag.State = true;
        }
        else
        {
            ViewBag.State = false;
        }
        return View();
    }

    [AllowAnonymous]
    [HttpGet("[action]/{userId}/{token}")] 
    public IActionResult UpdatePassword(string userId, string token)
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost("[action]/{userId}/{token}")] 
    public async Task<IActionResult> UpdatePassword(UpdatePasswordViewModel model, string userId, string token)
    {
        AppUser user = await _userManager.FindByIdAsync(userId);
        IdentityResult result =
            await _userManager.ResetPasswordAsync(user, HttpUtility.UrlDecode(token), model.Password);
        if (result.Succeeded)
        {
            ViewBag.State = true;
            await _userManager.UpdateSecurityStampAsync(user);
        }
        else
        {
            ViewBag.State = false;
        }

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> EditProfile(int Id=0)
    {
        AppUser user = null;
        if (Id == 0)
        {
            user = await _userManager.FindByNameAsync(User.Identity?.Name);
        }
        else
        {
            user = await _userManager.FindByIdAsync(Id.ToString());
        }
        UserViewModel userViewModel = new UserViewModel
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            UserIdendityNo = user.UserIdendityNo,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth,
            Note = user.Note
        };
        TempData["oldEmail"] = user.Email;
        return View(userViewModel);
    }

        [HttpPost]
        public async Task<IActionResult> EditProfile(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = null;
                AppUser user = null;
                if (model.Email.Equals(TempData["oldEmail"].ToString()))
                {
                    user =  await _userManager.FindByEmailAsync(model.Email);
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.UserName = model.UserName;
                    user.NormalizedUserName = model.UserName.ToUpper();
                    user.UserIdendityNo = model.UserIdendityNo;
                    user.PhoneNumber = model.PhoneNumber;
                    user.DateOfBirth = model.DateOfBirth;
                    user.Note = model.Note;
                    result = await _userManager.UpdateAsync(user);
                }
                else
                {
                    user =  _userManager.FindByEmailAsync(TempData["oldEmail"].ToString()).Result;
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.UserName = model.UserName;
                    user.NormalizedUserName = model.UserName.ToUpper();
                    user.UserIdendityNo = model.UserIdendityNo;
                    user.PhoneNumber = model.PhoneNumber;
                    user.Email = model.Email;
                    user.NormalizedEmail = model.Email.ToUpper();
                    user.DateOfBirth = model.DateOfBirth;
                    user.Note = model.Note;
                    result = await _userManager.UpdateAsync(user);
                    var token = await _userManager.GenerateChangeEmailTokenAsync(user,user.Email);
                    result = await _userManager.ChangeEmailAsync(user,user.Email,token);
                }
                if (!result.Succeeded)
                {
                    result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                    return View(model);
                }
                await _userManager.UpdateSecurityStampAsync(user);
                if (User.Identity.Name==TempData["oldEmail"].ToString() && model.Email != TempData["oldEmail"].ToString())
                {
                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(user, true);
                }
                TempData["EditProfileSuccess"] = true;
                return RedirectToAction("Index", "UserOperation", new { area = "Admin" });
            }
          
            return View(model);
        }
    
    [HttpGet]
    public IActionResult EditPassword()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> EditPassword(EditPasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (await _userManager.CheckPasswordAsync(user, model.OldPassword))
            {
                IdentityResult result =
                    await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (!result.Succeeded)
                {
                    result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                    return View(model);
                }

                await _userManager.UpdateSecurityStampAsync(user);
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(user, true);
            }
        }

        return RedirectToAction("Index", "Home", new { area = "Admin" });
    }


    [HttpGet]
    public async Task<IActionResult> Profile(int Id = 0)
    {
        AppUser user = null;
        if (Id == 0)
        {
            user = await _userManager.FindByNameAsync(User.Identity?.Name);
        }
        else
        {
            user = await _userManager.FindByIdAsync(Id.ToString());
        }

        UserViewModel userViewModel = new UserViewModel
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            UserIdendityNo = user.UserIdendityNo,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth,
            Note = user.Note
        };

        List<UserAddressViewModel> userAddressViewModel = new List<UserAddressViewModel>();
        foreach (var address in user.Addresses)
        {
            userAddressViewModel.Add(
                new UserAddressViewModel() {
                                                    Note = address.Note, 
                                                    AddressTitle=address.AddressTitle,
                                                    AddressType = address.AddressType,
                                                    Street=address.Street,
                                                    MainStreet=address.MainStreet,
                                                    NeighborhoodOrVillage=address.NeighborhoodOrVillage,
                                                    District=address.District,
                                                    City=address.City,
                                                    Country=address.Country,
                                                    RegionOrState=address.Country,
                                                    BuildingNo=address.BuildingNo,
                                                    FlatNo=address.FlatNo,
                                                    PostalCode=address.PostalCode,
                                                    AddressDetails=address.AddressDetails
            });
        }

        UserDetailViewModel userDetailViewModel = new UserDetailViewModel()
        {
           UserViewModel = userViewModel,
           UserAddressViewModels = userAddressViewModel
        };
        
        return View(userDetailViewModel);
    }
}