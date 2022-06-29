using System.ComponentModel.DataAnnotations;

namespace ECommerce.MVC.Areas.Admin.Models.ViewModels.Account;

public class RoleViewModel
    {
        [Display(Name = "Rol Adı")]
        public string Name { get; set; }
    }
