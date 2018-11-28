using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoCompany_1_1.Models
{
    public class User
    {
        [Required(ErrorMessage = "login required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "huy")]
        public string login { get; set; }
        public string password { get; set; }
        public string surname { get; set; }
        public string name { get; set; }
        public string patronymic { get; set; }
        public string phoneNumber { get; set; }

    }
}