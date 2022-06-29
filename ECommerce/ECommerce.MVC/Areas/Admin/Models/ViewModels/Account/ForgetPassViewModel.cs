using System.ComponentModel.DataAnnotations;

namespace ECommerce.MVC.Areas.Admin.Models.ViewModels.Account;


public class ForgetPassViewModel
    {
        [Display(Name = "E-Posta Adresiniz")]
        public string Email { get; set; }
    }
