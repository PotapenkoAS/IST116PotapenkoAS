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
        [StringLength(30, MinimumLength = 3)]
        public string login { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
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
            if (workerCode == null)
            {
                workerCode = "";
            }
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
                return "customer";
        }
        public List<string> Validation()
        {
            List<string> errors = new List<string>();
            if (login == null || login.Length < 4 || login.Length > 30)
            {
                errors.Add("Неверный логин");
            }
            if (password == null || password.Length < 4 || password.Length > 30)
            {
                errors.Add("Неверный пароль");
            }
            if (name == null)
            {
                errors.Add("Укажите имя");
            }
            if (surname == null)
            {
                errors.Add("Укажите фамилию");
            }
            if (phoneNumber == null)
            {
                errors.Add("Укажите номер телефона");
            }
            if (workerCode == null)
            {
                errors.Add("Укажите код работника");
            }
            if (errors.Count > 0)
            {
                return errors;
            }
            else
            {
                return null;
            }
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

        public static User Find(string login)
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
                    else
                    {
                        user.workerCode = "";
                        return user;
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
                    Console.Write(e.StackTrace);
                }
            }
            return user;
        }
        public static bool Update(User oldUser, User newUser)
        {
            try
            {
                using (AutoCompanyDBEntities ent = new AutoCompanyDBEntities())
                {
                    string query = string.Format("update {0} set login=@login,name=@name,surname=@surname,patronymic=@patronymic,phoneNumber=@phoneNumber where login=@oldLogin", "[" + oldUser.getRole() + "]");
                    ent.Database.ExecuteSqlCommand(query,
                        new SqlParameter("@login", newUser.login),
                        new SqlParameter("@name", newUser.name),
                        new SqlParameter("@surname", newUser.surname),
                        new SqlParameter("@patronymic", newUser.patronymic),
                        new SqlParameter("@phoneNumber", newUser.phoneNumber),
                        new SqlParameter("@oldLogin", oldUser.login)
                        );
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static string GenerateCode(string role)
        {
            Regex adminReg = new Regex(@"^[\.\$\^\{\[\(\|\)\*\+\?\\]");//  .$^{](|)]}?\     (|) 33-125 i
            string code;
            string syms = @".$^{](|)]}?\";
            Random rnd = new Random();
            using (AutoCompanyDBEntities ent = new AutoCompanyDBEntities())
            {
                switch (role)
                {
                    case "admin":
                        List<string> adminCodes = (from a in ent.admin select a.workerCode).ToList();
                        do
                        {
                            code = syms[rnd.Next(syms.Length)].ToString();
                            for (int i = 0; i < 6; i++)
                            {
                                code += Convert.ToChar(rnd.Next(33, 126)).ToString();
                            }
                        } while (adminCodes.Contains(code));
                        break;
                    case "driver":
                        List<string> driverCodes = (from a in ent.driver select a.workerCode).ToList();
                        do
                        {
                            code = rnd.Next(10).ToString();
                            for (int i = 0; i < 6; i++)
                            {
                                code += Convert.ToChar(rnd.Next(33, 126)).ToString();
                            }
                        } while (driverCodes.Contains(code));
                        break;
                    case "dispatcher":
                        List<string> dispatherCodes = (from a in ent.admin select a.workerCode).ToList();
                        do
                        {
                            code = Convert.ToChar(rnd.Next(97, 123)).ToString();
                            for (int i = 0; i < 6; i++)
                            {
                                code += Convert.ToChar(rnd.Next(33, 126)).ToString();
                            }
                        } while (dispatherCodes.Contains(code));
                        break;
                    default:
                        return null;
                }
            }
            return code;
        }
    }
}