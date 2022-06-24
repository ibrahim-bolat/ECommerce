using ECommerce.Entities.Concrete.Identity.Entities;
using ECommerce.MVC.Areas.Admin.Models.ViewModels.UserOperation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace ECommerce.MVC.Areas.Admin.Controllers;


[Area("Admin")]
    public class UserOperationController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserOperationController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, SignInManager<AppUser>  signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Users()
        {
            try
            {
                var userData = _userManager.Users.AsQueryable();
                ;
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request
                    .Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length == "-1" ? userData.Count() : length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    userData = userData.OrderBy(s => sortColumn + " " + sortColumnDirection);
                    Func<AppUser, string> orderingFunction = (c => sortColumn == "FirstName" ? c.FirstName :
                        sortColumn == "LastName" ? c.LastName :
                        sortColumn == "UserName" ? c.UserName :
                        sortColumn == "Email" ? c.Email : c.FirstName);

                    if (sortColumnDirection == "desc")
                    {
                        userData = userData.OrderByDescending(orderingFunction).AsQueryable();
                    }
                    else
                    {
                        userData = userData.OrderBy(orderingFunction).AsQueryable();
                    }
                }

                if (!string.IsNullOrEmpty(searchValue))
                {
                    userData = userData.Where(m => m.FirstName.ToLower().Contains(searchValue.ToLower())
                                                   || m.LastName.ToLower().Contains(searchValue.ToLower())
                                                   || m.UserName.ToLower().Contains(searchValue.ToLower())
                                                   || m.Email.ToLower().Contains(searchValue.ToLower()));
                }

                recordsTotal = userData.Count();
                var data = userData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new
                {
                    draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data,
                    isSusccess = true
                };
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserBriefDetailsViewModel userBriefDetailsViewModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("PartialViews/_UserCreateModalPartial", userBriefDetailsViewModel);
            }

            IdentityResult createResult, confirmResult = null;
            AppUser newUsers = new AppUser
            {
                FirstName = userBriefDetailsViewModel.Name,
                LastName = userBriefDetailsViewModel.SurName,
                UserName = userBriefDetailsViewModel.UserName,
                Email = userBriefDetailsViewModel.Email
            };
            createResult = await _userManager.CreateAsync(newUsers, userBriefDetailsViewModel.Password);
            if (createResult.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUsers);
                confirmResult = await _userManager.ConfirmEmailAsync(newUsers, token);
                if (confirmResult.Succeeded)
                {
                    return Json(new { success = true });
                }

                confirmResult.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
            }

            createResult.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));

            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value.Errors);
            return Json(new { success = false, errors = errors });
        }

        [HttpGet]
        public async Task<IActionResult> GetbyID(string ID)
        {
            AppUser appUser = await _userManager.FindByIdAsync(ID);
            if (appUser != null)
            {
                UserBriefDetailsViewModel model = new UserBriefDetailsViewModel
                {
                    Id = appUser.Id.ToString(),
                    Name = appUser.FirstName,
                    SurName = appUser.LastName,
                    UserName = appUser.UserName,
                    Email = appUser.Email,
                    Password = null,
                    RePassword = null
                };
                return Json(new { success = true, user = model });
            }

            ModelState.AddModelError("userFindError",
                "Bu bilgilere sahip bir kullanıcı bulunamadı.");

            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value.Errors);
            return Json(new { success = false, errors = errors });
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserBriefDetailsViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("PartialViews/_UserUpdateModalPartial", userViewModel);
            }

            IdentityResult resetResult, confirmResult = null;
            AppUser updatedUser = await _userManager.FindByIdAsync(userViewModel.Id);
            if (updatedUser != null)
            {
                updatedUser.FirstName = userViewModel.Name;
                updatedUser.LastName = userViewModel.SurName;
                updatedUser.UserName = userViewModel.UserName;
                updatedUser.Email = userViewModel.Email;

                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(updatedUser);
                resetResult =
                    await _userManager.ResetPasswordAsync(updatedUser, resetToken, userViewModel.Password);

                if (resetResult.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(updatedUser);
                    confirmResult = await _userManager.ConfirmEmailAsync(updatedUser, token);

                    if (confirmResult.Succeeded)
                    {
                        return Json(new { success = true });
                    }

                    confirmResult.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                }

                resetResult.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
            }

            ModelState.AddModelError("userFindError",
                "Bu bilgilere sahip bir kullanıcı bulunamadı.");

            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value.Errors);
            return Json(new { success = false, errors = errors });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string ID)
        {
            IdentityResult result = null;
            AppUser deletedUser = await _userManager.FindByIdAsync(ID);
            if (deletedUser != null)
                result = await _userManager.DeleteAsync(deletedUser);
            if (result.Succeeded)
            {
                return Json(new { success = true });
            }

            result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));

            var errors = ModelState.ToDictionary(x => x.Key, x => x.Value.Errors);
            return Json(new { success = false, errors = errors });
        }

        [HttpGet]
        public async Task<IActionResult> GetRole(string Id)
        {
            AppUser user = await _userManager.FindByIdAsync(Id);
            List<AppRole> allRoles = _roleManager.Roles.ToList();
            List<string> userRoles = await _userManager.GetRolesAsync(user) as List<string>;
            List<RoleOperationViewModel> assignRoles = new List<RoleOperationViewModel>();
            allRoles.ForEach(role => assignRoles.Add(new RoleOperationViewModel
            {
                Id = role.Id,
                Name = role.Name,
                HasAssign = userRoles.Contains(role.Name)
            }));
            return Json(new { success = true, roles = assignRoles });
        }

        [HttpPost]
        public async Task<IActionResult> SaveRole(string Id, List<String> roles)
        {
            if (!string.IsNullOrEmpty(Id) && roles != null)
            {
                AppUser user = await _userManager.FindByIdAsync(Id);
                List<AppRole> allRoles = _roleManager.Roles.ToList();
                List<RoleOperationViewModel> newAssignRoles = new List<RoleOperationViewModel>();
                List<string> userNewRoles = new List<string>();
                AppRole appR = null;
                foreach (var role in roles)
                {
                    appR = await _roleManager.FindByIdAsync(role);
                    userNewRoles.Add(appR.Name);
                }
                allRoles.ForEach(role => newAssignRoles.Add(new RoleOperationViewModel
                {
                    HasAssign = userNewRoles.Contains(role.Name),
                    Id = role.Id,
                    Name = role.Name
                }));
                foreach (RoleOperationViewModel role in newAssignRoles)
                {
                    if (role.HasAssign)
                    {
                        if (!await _userManager.IsInRoleAsync(user, role.Name))
                            await _userManager.AddToRoleAsync(user, role.Name);
                    }
                    else
                    {
                        if (await _userManager.IsInRoleAsync(user, role.Name))
                            await _userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                }
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                if (currentUser.Id == user.Id)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(user, true);
                }

                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }