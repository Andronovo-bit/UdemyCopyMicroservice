﻿using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class SigninInput
    {
        [Display(Name="Email Adresiniz")]
        public string Email { get; set; }
        [Display(Name = "Şifreniz")]
        public string Password { get; set; }
        [Display(Name = "Beni Hatırla")]
        public bool RememberMe { get; set; }
    }
}
