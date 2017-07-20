using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationRSA.Models
{
    public class LoginModel
    {
        public string email { get; set; }
        public string plainTextPassword { get; set; }
        public string encryptedPassword { get; set; }
        public string rememberMe { get; set; }
    }
}