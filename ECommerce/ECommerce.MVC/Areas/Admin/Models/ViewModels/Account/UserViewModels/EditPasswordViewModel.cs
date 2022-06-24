using System.ComponentModel.DataAnnotations;

namespace ECommerce.MVC.Areas.Admin.Models.ViewModels.Account.UserViewModels;

public class EditPasswordViewModel
        {
            [Required(ErrorMessage = "Lütfen şifreyi boş geçmeyiniz...")]
            [DataType(DataType.Password, ErrorMessage = "Lütfen şifreyi tüm kuralları göz önüne alarak giriniz...")]
            [Display(Name = "Eski Şifre")]
            public string OldPassword { get; set; }

            [Required(ErrorMessage = "Lütfen şifreyi boş geçmeyiniz...")]
            [DataType(DataType.Password, ErrorMessage = "Lütfen şifreyi tüm kuralları göz önüne alarak giriniz...")]
            [Display(Name = "Yeni Şifre")]
            public string NewPassword { get; set; }
            
            [Required(ErrorMessage = "Lütfen tekrar şifreyi boş geçmeyiniz...")]
            [DataType(DataType.Password, ErrorMessage = "Lütfen tekrar şifreyi tüm kuralları göz önüne alarak giriniz...")]
            [Compare("NewPassword", ErrorMessage = "Lütfen şifre ile aynı giriniz...")]
            [Display(Name = "Yeni Şifre Tekrar")]
            public string ReNewPassword { get; set; }
        }

