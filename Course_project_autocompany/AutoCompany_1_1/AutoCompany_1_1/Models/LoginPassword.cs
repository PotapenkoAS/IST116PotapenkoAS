using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoCompany_1_1.Models
{
    public class LoginPassword
    {
        [Required]
        public string login { get; set; }
        [Required]
        [StringLength(maximumLength:50, MinimumLength = 4)]
        public string password { get; set; }

        public LoginPassword(string login,string password)
        {
            this.login = login;
            this.password = password;
        }
        public LoginPassword()
        {
            
        }
    }
}