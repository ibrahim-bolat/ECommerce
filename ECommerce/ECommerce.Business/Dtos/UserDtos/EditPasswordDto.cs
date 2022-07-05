using System.ComponentModel.DataAnnotations;
using ECommerce.Shared.Entities.Abtract;
using ECommerce.Shared.Entities.Concrete;


namespace ECommerce.Business.Dtos.UserDtos;

public class EditPasswordDto:BaseDto,IDto
        {

            [Display(Name = "Yeni Şifre")]
            public string NewPassword { get; set; }

            [Display(Name = "Yeni Şifre Tekrar")]
            public string ReNewPassword { get; set; }
        }

