using System;
using System.IO;

namespace ECommerce.MVC.Areas.Admin.Models.ViewModels.Account.RoleViewModels;


public class RoleAssignViewModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool HasAssign { get; set; }
    }