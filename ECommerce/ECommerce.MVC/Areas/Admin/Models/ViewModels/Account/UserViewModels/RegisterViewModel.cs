using System.ComponentModel.DataAnnotations;

namespace ECommerce.MVC.Areas.Admin.Models.ViewModels.Account.UserViewModels;


public class RegisterViewModel
    {
        [Required(ErrorMessage = "Lütfen adınızı boş geçmeyiniz...")]
        [Display(Name = "Ad")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lütfen soyadınızı boş geçmeyiniz...")]
        [Display(Name = "Soyad")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Lütfen kullanıcı adını boş geçmeyiniz...")]
        [StringLength(15, ErrorMessage = "Lütfen kullanıcı adını 4 ile 15 karakter arasında giriniz...", MinimumLength = 4)]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
        
        
        [Required(ErrorMessage = "Lütfen emaili boş geçmeyiniz...")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Lütfen uygun formatta e-posta adresi giriniz.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        
        [Required(ErrorMessage = "Lütfen şifreyi boş geçmeyiniz...")]
        [DataType(DataType.Password, ErrorMessage = "Lütfen şifreyi tüm kuralları göz önüne alarak giriniz...")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Lütfen tekrar şifreyi boş geçmeyiniz...")]
        [DataType(DataType.Password, ErrorMessage = "Lütfen tekrar şifreyi tüm kuralları göz önüne alarak giriniz...")]
        [Compare("Password", ErrorMessage = "Lütfen şifre ile aynı giriniz...")]
        [Display(Name = "Şifre Tekrar")]
        public string RePassword { get; set; }
    }

