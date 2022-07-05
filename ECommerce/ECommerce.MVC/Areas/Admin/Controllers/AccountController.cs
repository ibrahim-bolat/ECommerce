using System.Web;
using ECommerce.Business.Dtos.AddressDtos;
using ECommerce.Business.Dtos.UserDtos;
using ECommerce.Business.ValidationRules.FluentValidation.Account;
using ECommerce.Entities.Concrete.Identity.Entities;
using ECommerce.Helpers.MailHelper;
using ECommerce.Shared.Service.Abtract;
using FluentValidation.Results;
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
    public async Task<IActionResult> Register(RegisterDto model)
    {
        if (ModelState.IsValid)
        {
            AppUser applicationUser = new AppUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email
            };
            AppRole role = await _roleManager.FindByNameAsync("User");
            if (role == null)
                await _roleManager.CreateAsync(new AppRole { Name = "User" });
            IdentityResult userResult = await _userManager.CreateAsync(applicationUser, model.Password);
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
                return View(model);
            }
            userResult.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
            return View(model);
        }
        return View(model);
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
    public IActionResult Login(string returnUrl = "Index")
    {
        TempData["returnUrl"] = returnUrl;
        return View();
    }

    [AllowAnonymous]
    [HttpPost] 
    public async Task<IActionResult> Login(LoginDto model)
    {
        if (ModelState.IsValid)
        {
            AppUser user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                await _signInManager.SignOutAsync();
                SignInResult result =
                    await _signInManager.PasswordSignInAsync(user, model.Password, model.Persistent, model.Lock);

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
                            return View(model);
                        }
                        ModelState.AddModelError("InvalidEpostaOrPass1", "E-posta veya şifre yanlış.");
                        return View(model);
                    }
                }
            }
            ModelState.AddModelError("NoUser", "Böyle bir kullanıcı bulunmamaktadır.");
            ModelState.AddModelError("InvalidEpostaOrPass2", "E-posta veya şifre yanlış.");
            return View(model);
        }
        return View(model);
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
    public async Task<IActionResult> ForgetPass(ForgetPassDto model)
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
    public async Task<IActionResult> UpdatePassword(UpdatePasswordDto model, string userId, string token)
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
    public async Task<IActionResult> EditProfile(int id)
    {
        if (id != 0)
        {
            AppUser user = await _userManager.FindByIdAsync(id.ToString());
            UserDto userDto = new UserDto
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
            return View(userDto);
        }
        return RedirectToAction("AccessDenied", "ErrorPages");
    }

        [HttpPost]
        public async Task<IActionResult> EditProfile(UserDto model)
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
                    user =  await _userManager.FindByEmailAsync(TempData["oldEmail"].ToString());
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
    public IActionResult EditPassword(int id)
    {
        if (id != 0)
        {
            ViewBag.userId = id;
            return View();
        }
        return RedirectToAction("AccessDenied", "ErrorPages");
    }
    
    [HttpPost]
    public async Task<IActionResult> EditPassword(EditPasswordDto model,int id)
    {
        if (ModelState.IsValid)
        {
            if (id != 0)
            {
                AppUser user = await _userManager.FindByIdAsync(id.ToString());
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                IdentityResult result  = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                if (!result.Succeeded)
                {
                    result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                    return View(model);
                }
                await _userManager.UpdateSecurityStampAsync(user);
                if (User.Identity.Name==user.UserName)
                {
                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(user, true);
                }
                TempData["EditPasswordSuccess"] = true;
                return RedirectToAction("Index", "UserOperation", new { area = "Admin" });
            }
            return RedirectToAction("AccessDenied", "ErrorPages");
        }
        return View(model);
    }


    [HttpGet]
    public async Task<IActionResult> Profile(int id)
    {
        if (id != 0)
        {
            AppUser user = await _userManager.FindByIdAsync(id.ToString());
            UserDto UserDto = new UserDto
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
            List<AddressSummaryDto> addressSummaryDtos = new List<AddressSummaryDto>();
            foreach (var address in user.Addresses)
            {
                addressSummaryDtos.Add(
                    new AddressSummaryDto() {
                        Id = address.Id, 
                        AddressTitle=address.AddressTitle,
                        AddressDetails=address.AddressDetails,
                        DefaultAddress = address.DefaultAddress
                    });
            }
            UserDetailDto userDetailDto = new UserDetailDto()
            {
                UserDto = UserDto,
                UserAddressSummaryDtos = addressSummaryDtos
            };
            return View(userDetailDto);
        }
        return RedirectToAction("AccessDenied", "ErrorPages");
    }
}