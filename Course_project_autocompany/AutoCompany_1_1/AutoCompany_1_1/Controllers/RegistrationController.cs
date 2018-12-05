using System.Collections.Generic;
using System.Web.Mvc;
using AutoCompany_1_1.Models;

namespace AutoCompany_1_1.Controllers
{
    public class RegistrationController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(User user)
        {
            using (AutoCompanyDBEntities ent = new AutoCompanyDBEntities())
            {
                if (user.workerCode == null)
                {
                    user.workerCode = "";
                }
                List<string> errors = user.Validation();
                if (errors == null)
                {
                    if (Models.User.Find(user.login) == null)
                    {
                        Session["User"] = user;
                        ent.customer.Add(customer.Convert(user));
                        ent.SaveChanges();
                    }
                    else
                    {
                        errors.Add("Пользователь с таким логином уже существует");
                        ViewData["ErrorMessage"] = errors;
                        return View();
                    }
                }
                else
                {
                    ViewData["ErrorMessage"] = errors;
                    return View();
                }
            }
            return RedirectToAction("..");
        }
    }
}