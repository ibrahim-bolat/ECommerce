using System.ComponentModel.DataAnnotations;
using ECommerce.Shared.Entities.Abtract;

namespace ECommerce.Business.Dtos.UserDtos;


public class ForgetPassDto:BaseDto,IDto
    {
        [Display(Name = "E-Posta Adresiniz")]
        public string Email { get; set; }
    }
