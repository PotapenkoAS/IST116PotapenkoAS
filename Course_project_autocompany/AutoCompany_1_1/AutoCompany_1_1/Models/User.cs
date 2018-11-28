using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoCompany_1_1.Models
{
    public class User
    {
        public string login { get; set; }
        public string password { get; set; }
        public string surname { get; set; }
        public string name { get; set; }
        public string patronymic { get; set; }
        public string phoneNumber { get; set; }
    }
}