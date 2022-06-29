using System.ComponentModel.DataAnnotations;

namespace ECommerce.MVC.Areas.Admin.Models.ViewModels.Account;

public class EditPasswordViewModel
        {
            [Display(Name = "Eski Şifre")]
            public string OldPassword { get; set; }
            
            [Display(Name = "Yeni Şifre")]
            public string NewPassword { get; set; }

            [Display(Name = "Yeni Şifre Tekrar")]
            public string ReNewPassword { get; set; }
        }

