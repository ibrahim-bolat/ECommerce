using System.ComponentModel.DataAnnotations;
using ECommerce.Shared.Entities.Abtract;
using ECommerce.Shared.Entities.Concrete;

namespace ECommerce.Business.Dtos.UserDtos;


public class LoginDto:BaseDto,IDto
    {
        [Display(Name = "E-Posta ")]
        public string Email { get; set; }
        

        [Display(Name = "Şifre")]
        public string Password { get; set; }
        

        [Display(Name = "Beni Hatırla")]
        public bool Persistent { get; set; }
        
        public bool Lock { get; set; }
    }
