using System.ComponentModel.DataAnnotations;
using ECommerce.Shared.Entities.Abtract;

namespace ECommerce.Business.Dtos.UserDtos;


public class UserSummaryDto:BaseDto,IDto
    {
        public string Id { get; set; }

        [Display(Name = "Ad")]
        public string FirstName { get; set; }
        
        [Display(Name = "Soyad")]
        public string LastName { get; set; }
        
        
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }


        [Display(Name = "Email")]
        public string Email { get; set; }

    }