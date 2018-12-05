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
        public static bool Update(User oldUser,User newUser)
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                using (conn = new SqlConnection("Data Source=(localDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\AtApse2\\source\\repos\\AutoCompany_1_1\\AutoCompany_1_1\\App_Data\\AutoCompanyDB.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework"))
                {
                    conn.Open();
                    //string query = "update @table set login=@login,name=@name,surname=@surname,patronymic=@patronymic,phoneNumber=@phoneNumber where login=@oldLogin";
                    string query = String.Format("update {0} set login=@login,name=@name,surname=@surname,patronymic=@patronymic,phoneNumber=@phoneNumber where login=@oldLogin", "[" + oldUser.getRole() + "]");
                    SqlCommand command = new SqlCommand(query, conn);
                   // command.Parameters.AddWithValue("@table", "["+oldUser.getRole()+"]");
                    command.Parameters.AddWithValue("@login",newUser.login);
                    command.Parameters.AddWithValue("@name",newUser.name);
                    command.Parameters.AddWithValue("@surname",newUser.surname);
                    command.Parameters.AddWithValue("@patronymic",newUser.patronymic);
                    command.Parameters.AddWithValue("@phoneNumber",newUser.phoneNumber);
                    command.Parameters.AddWithValue("@oldLogin",oldUser.login);
                    command.ExecuteNonQuery();  
                }
                return true;
            }
            catch (Exception e)
            { 
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

    }
}