using System.Web;
using ECommerce.Entities.Concrete.Identity.Entities;
using ECommerce.Helpers.MailHelper;
using ECommerce.MVC.Areas.Admin.Models.ViewModels.Account.UserViewModels;
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
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        if (ModelState.IsValid)
        {
            AppUser applicationUser = new AppUser
            {
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email
            };
            AppRole role = await _roleManager.FindByNameAsync("User");
            if (role == null)
                await _roleManager.CreateAsync(new AppRole { Name = "User" });
            IdentityResult userResult = await _userManager.CreateAsync(applicationUser, registerViewModel.Password);
            IdentityResult roleResult = await _userManager.AddToRoleAsync(applicationUser, "User");
            if (userResult.Succeeded && roleResult.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
                var confirmationLink = Url.Action("ConfirmEmail", "Account",
                    new { area = "Admin", token = HttpUtility.UrlEncode(token), email = registerViewModel.Email },
                    Request.Scheme);

                MailRequest mailRequest = new MailRequest
                {
                    ToMail = registerViewModel.Email,
                    ConfirmationLink = confirmationLink,
                    MailSubject = "Mail Adresini Onayla",
                    IsBodyHtml = true,
                    MailLinkTitle = "Emailinizi Doğrulamak İçin tıklayınız"
                };
                bool emailResponse = _emailService.SendEmail(mailRequest);

                if (emailResponse)
                {
                    TempData["LoginSuccess"] = true;
                    return RedirectToAction("Login", "Account", new { area = "Admin" });
                }
                else
                {
                    // log email failed 
                }
            }
            else
            {
                userResult.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                roleResult.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
            }
        }

        return View();
    }

    [AllowAnonymous]
    [HttpGet("[action]/{email}/{token}")] //kullanıcı email onaylama dolu sayfa
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
    [HttpGet] //kullanıcı boş giriş sayfası
    public IActionResult Login(string ReturnUrl = "Index")
    {
        TempData["returnUrl"] = ReturnUrl;
        return View();
    }

    [AllowAnonymous]
    [HttpPost] //kullanıcı dolu giriş sayfası
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            AppUser user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                //İlgili kullanıcıya dair önceden oluşturulmuş bir Cookie varsa siliyoruz.
                await _signInManager.SignOutAsync();
                SignInResult result =
                    await _signInManager.PasswordSignInAsync(user, model.Password, model.Persistent, model.Lock);

                if (result.Succeeded)
                {
                    await _userManager.ResetAccessFailedCountAsync(
                        user); //Önceki hataları girişler neticesinde +1 arttırılmış tüm değerleri 0(sıfır)a çekiyoruz.
                    if (string.IsNullOrEmpty(TempData["returnUrl"] != null ? TempData["returnUrl"].ToString() : ""))
                        return RedirectToAction("Index", "Home");
                    if (TempData["returnUrl"].Equals("Index") || TempData["returnUrl"].Equals("/"))
                        return RedirectToAction("Index", "Home");
                    bool emailStatus = await _userManager.IsEmailConfirmedAsync(user);
                    if (emailStatus == false)
                    {
                        ModelState.AddModelError(nameof(model.Email), "Email Doğrulanamadı");
                    }

                    return Redirect(TempData["returnUrl"].ToString());
                }
                else
                {
                    await _userManager
                        .AccessFailedAsync(
                            user); //Eğer ki başarısız bir account girişi söz konusu ise AccessFailedCount kolonundaki değer +1 arttırılacaktır. 

                    int failcount =
                        await _userManager
                            .GetAccessFailedCountAsync(
                                user); //Kullanıcının yapmış olduğu başarısız giriş deneme adedini alıyoruz.
                    if (failcount == 3)
                    {
                        await _userManager.SetLockoutEndDateAsync(user,
                            new DateTimeOffset(DateTime.Now
                                .AddMinutes(
                                    30))); //Eğer ki başarısız giriş denemesi 3'ü bulduysa ilgili kullanıcının hesabını kilitliyoruz.
                        ModelState.AddModelError("Locked",
                            "Art arda 3 başarısız giriş denemesi yaptığınızdan dolayı hesabınız 30 dk kitlenmiştir.");
                    }
                    else
                    {
                        if (result.IsLockedOut)
                            ModelState.AddModelError("Locked",
                                "Art arda 3 başarısız giriş denemesi yaptığınızdan dolayı hesabınız 30 dk kilitlenmiştir.");
                        else
                            ModelState.AddModelError("NotUser2", "E-posta veya şifre yanlış.");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("NotUser", "Böyle bir kullanıcı bulunmamaktadır.");
                ModelState.AddModelError("NotUser2", "E-posta veya şifre yanlış.");
            }
        }

        return View(model);
    }

    [HttpGet] //kullanıcı çıkış
    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }

    [AllowAnonymous]
    [HttpGet] //kullanıcı parola restleme boş sayfa
    public IActionResult ForgetPass()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost] //kullanıcı parola resteleme dolu sayfa
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
                ViewBag.State = true;
            else
            {
                ViewBag.State = false;
            }

            ViewBag.State = true;
        }
        else
            ViewBag.State = false;

        return View();
    }

    [AllowAnonymous]
    [HttpGet("[action]/{userId}/{token}")] //kullanıcı parola güncelleme boş sayfa
    public IActionResult UpdatePassword(string userId, string token)
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost("[action]/{userId}/{token}")] //kullanıcı parola güncelleme dolu sayfa
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
    public async Task<IActionResult> EditProfile()
    {
        AppUser user = await _userManager.FindByNameAsync(User.Identity?.Name);
        UserDetailViewModel userDetail = new UserDetailViewModel
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            UserName = user.UserName,
            Email = user.Email
        };
        return View(userDetail);
    }

    [HttpPost]
    public async Task<IActionResult> EditProfile(UserDetailViewModel model)
    {
        if (ModelState.IsValid)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            IdentityResult result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                return View(model);
            }

            //burda kullanıcının “SecurityStamp” değerini güncelleyip ve ardından kullanıcıya çıkış yaptırıp,
            //tekrar giriş yaptırıyoruz
            await _userManager.UpdateSecurityStampAsync(user);
            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(user, true);
        }

        return RedirectToAction("Index", "Home", new { area = "Admin" });
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

        UserDetailViewModel userDetail = new UserDetailViewModel
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            UserIdendityNo = user.UserIdendityNo,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth

        };
        return View(userDetail);
    }
}