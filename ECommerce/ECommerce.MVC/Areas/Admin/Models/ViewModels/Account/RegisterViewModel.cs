﻿using System.ComponentModel.DataAnnotations;

namespace ECommerce.MVC.Areas.Admin.Models.ViewModels.Account;


public class RegisterViewModel
    {
        [Display(Name = "Ad")]
        public string FirstName { get; set; }
        
        [Display(Name = "Soyad")]
        public string LastName { get; set; }
        
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
        
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [Display(Name = "Şifre Tekrar")]
        public string RePassword { get; set; }
    }
