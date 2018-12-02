using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoCompany_1_1.Models
{
    public class User
    {
        [Required]
        public string login { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string surname { get; set; }
        [Required]
        public string name { get; set; }
        public string patronymic { get; set; }
        [Required]
        public string phoneNumber { get; set; }
        public string workerCode { get; set; }
    }
}