using System.ComponentModel.DataAnnotations;
using ECommerce.Shared.Entities.Abtract;

namespace ECommerce.Business.Dtos.UserDtos;


public class UpdatePasswordDto:BaseDto,IDto
    {
        [Display(Name = "Yeni Şifre")]
        public string Password { get; set; }
        
        [Display(Name = "Yeni Şifre Tekrar")]
        public string RePassword { get; set; }
    }
