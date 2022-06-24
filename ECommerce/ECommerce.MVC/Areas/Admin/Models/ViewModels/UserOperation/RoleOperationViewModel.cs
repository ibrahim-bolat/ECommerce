using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MVC.Areas.Admin.Models.ViewModels.UserOperation;


public class RoleOperationViewModel
    {
        
        [Display(Name = "Id")]
        [HiddenInput]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Rolü boş geçmeyiniz.")]
        [Display(Name = "Rol Adı")]
        public string Name { get; set; }
        
        
        public bool HasAssign { get; set; }
    }