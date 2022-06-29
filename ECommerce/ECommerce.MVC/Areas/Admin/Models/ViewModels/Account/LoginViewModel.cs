using System.ComponentModel.DataAnnotations;

namespace ECommerce.MVC.Areas.Admin.Models.ViewModels.Account;


public class LoginViewModel
    {
        [Display(Name = "E-Posta ")]
        public string Email { get; set; }
        

        [Display(Name = "Şifre")]
        public string Password { get; set; }
        

        [Display(Name = "Beni Hatırla")]
        public bool Persistent { get; set; }
        
        public bool Lock { get; set; }
    }
