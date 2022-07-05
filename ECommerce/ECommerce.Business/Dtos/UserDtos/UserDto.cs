using System.ComponentModel.DataAnnotations;
using ECommerce.Business.ValidationRules.CustomValidation.Account;
using ECommerce.Shared.Entities.Abtract;
using ECommerce.Shared.Entities.Concrete;

namespace ECommerce.Business.Dtos.UserDtos;

public class UserDto:BaseDto,IDto
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
