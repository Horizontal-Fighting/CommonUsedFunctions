using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication_JS_RSA.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required.")]
        public string EncryptedPassword { get; set; }

        public string ReturnUrl { get; set; }

        public string RedirectDomain { get; set; }

        public string PublicKey { get; set; }

        public string RedirectUrl { get; set; }
        public string ErrorMessage { get; set; }

    }
}