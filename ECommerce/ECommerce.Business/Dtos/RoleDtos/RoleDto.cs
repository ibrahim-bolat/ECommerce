using System.ComponentModel.DataAnnotations;
using ECommerce.Shared.Entities.Abtract;
using ECommerce.Shared.Entities.Concrete;

namespace ECommerce.Business.Dtos.RoleDtos;

public class RoleDto:BaseDto,IDto
    {
        [Display(Name = "Rol Adı")]
        public string Name { get; set; }
    }
