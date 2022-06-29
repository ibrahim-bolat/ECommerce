using System.ComponentModel.DataAnnotations;
using ECommerce.Entities.Concrete;
using ECommerce.Entities.Enums;


namespace ECommerce.MVC.Areas.Admin.Models.ViewModels.Account;

public class UserDetailViewModel
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
        
        [DisplayFormat(DataFormatString="{0:dd.MM.yyyy}")]
        [Display(Name = "Doğum Tarihi")]
        public DateTime? DateOfBirth{get; set;}
        
    }
