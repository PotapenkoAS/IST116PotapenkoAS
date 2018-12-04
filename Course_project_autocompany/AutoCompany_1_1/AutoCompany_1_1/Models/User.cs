using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
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

        public string getRole()
        {
            Regex driverReg = new Regex(@"^\d");
            Regex dispatcherReg = new Regex(@"^[a-z]", RegexOptions.IgnoreCase);
            Regex adminReg = new Regex(@"^[\.\$\^\{\[\(\|\)\*\+\?\\]");
            if (driverReg.IsMatch(workerCode))
            {
                return "driver";
            }
            else
            if (dispatcherReg.IsMatch(workerCode))
            {
                return "dispatcher";
            }
            else
            if (adminReg.IsMatch(workerCode))
            {
                return "admin";
            }
            else
                return "user";
        }

        public static User getUserByLP(LoginPassword lp)
        {
            using (AutoCompanyDBEntities ent = new AutoCompanyDBEntities())
            {
                User user = new User();
                try
                {
                    user = ent.customer.Where(a => a.login == lp.login && a.password == lp.password).FirstOrDefault();
                    if (user == null)
                    {
                        user = ent.driver.Where(a => a.login == lp.login && a.password == lp.password).FirstOrDefault();
                    }
                    if (user == null)
                    {
                        user = ent.admin.Where(a => a.login == lp.login && a.password == lp.password).FirstOrDefault();
                    }
                    if (user == null)
                    {
                        user = ent.dispatcher.Where(a => a.login == lp.login && a.password == lp.password).FirstOrDefault();
                    }
                }
                catch (InvalidOperationException e)
                {
                    user = null;
                }
                return user;
            }
        }

        public static bool Contains(string login)
        {
            User user;
            using (AutoCompanyDBEntities ent = new AutoCompanyDBEntities())
            {
                try
                {
                    user = ent.customer.Where(a => a.login == login).FirstOrDefault();
                    if (user == null)
                    {
                        user = ent.admin.Where(a => a.login == login).FirstOrDefault();
                    }
                    if (user == null)
                    {
                        user = ent.driver.Where(a => a.login == login).FirstOrDefault();
                    }
                    if (user == null)
                    {
                        user = ent.dispatcher.Where(a => a.login == login).FirstOrDefault();
                    }
                }
                catch (InvalidOperationException e)
                {
                    user = null;
                }
            }
            if (user != null)
            {
                return true;
            }
            else {
                return false;
            }
        }
    }
}