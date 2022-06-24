using System.ComponentModel.DataAnnotations;


namespace ECommerce.MVC.Areas.Admin.Models.ViewModels.Account.UserViewModels;

public class UserDetailViewModel
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
        
        [Required(ErrorMessage = "Lütfen Kimlik Noyu boş geçmeyiniz...")]
        [StringLength(11, ErrorMessage = "Lütfen Kimlik Numaranızı 11 karakter giriniz...", MinimumLength = 11)]
        [Display(Name = "Kimlik No")]
        public string UserIdendityNo { get; set; }

        [Required(ErrorMessage = "Lütfen telefon numaranızı boş geçmeyiniz...")]
        [Display(Name = "Telefon")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Lütfen uygun formatta telefon giriniz.")]
        public string PhoneNumber { get; set; }
        
        [Required(ErrorMessage = "Lütfen emaili boş geçmeyiniz...")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Lütfen uygun formatta e-posta adresi giriniz.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen doğum tarihinizi boş geçmeyiniz...")]
        [DisplayFormat(DataFormatString="{0:dd.MM.yyyy}")]
        [Display(Name = "Doğum Tarihi")]
        public DateTime? DateOfBirth{get; set;}

    }
