using System.ComponentModel.DataAnnotations;
using ECommerce.Shared.Entities.Abtract;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Business.Dtos.RoleDtos;


public class RoleOperationDto:BaseDto,IDto
    {
        
        [Display(Name = "Id")]
        [HiddenInput]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Rolü boş geçmeyiniz.")]
        [Display(Name = "Rol Adı")]
        public string Name { get; set; }
        
        
        public bool HasAssign { get; set; }
    }