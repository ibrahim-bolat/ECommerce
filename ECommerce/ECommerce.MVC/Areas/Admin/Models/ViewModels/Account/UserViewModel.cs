using System.ComponentModel.DataAnnotations;
using ECommerce.MVC.Areas.Admin.ValidationRules.CustomValidation.Account;


namespace ECommerce.MVC.Areas.Admin.Models.ViewModels.Account;

public class UserViewModel
    {
        public int Id { get; set; }
        
        [Display(Name = "Ad")]
        public string FirstName { get; set; }
        
        [Display(Name = "Soyad")]
        public string LastName { get; set; }
        
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
        
        [Display(Name = "Kimlik No")]
        public string UserIdendityNo { get; set; }
        
        [Display(Name = "Telefon")]
        public string PhoneNumber { get; set; }
        
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:D}",ApplyFormatInEditMode = false)]
        [Display(Name = "Doğum Tarihi")]
        [CustomDate]
        public DateTime? DateOfBirth{get; set;}
        
        [Display(Name = "Not")]
        public string Note { get; set; }
        
    }
