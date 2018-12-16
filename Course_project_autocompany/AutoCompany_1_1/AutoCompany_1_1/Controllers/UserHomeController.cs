using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoCompany_1_1.Models;

namespace AutoCompany_1_1.Controllers
{
    public class UserHomeController : Controller
    {
        // GET: UserHome
        [HttpGet]
        public ActionResult Index()
        {
            using (AutoCompanyDBEntities ent = new AutoCompanyDBEntities()) {
                User user = Session["User"] as User;
                if (user!=null && user.getRole() == "driver")
                {
                    user = ent.driver.Include("qualification").Where(a => a.workerCode == user.workerCode).FirstOrDefault();
                }
                ViewData["idQualification"] = new List<SelectListItem>();
                List<qualification> qlist = ent.qualification.ToList();
                foreach(qualification el in qlist)
                {
                    SelectListItem item = new SelectListItem()
                    {
                        Value = el.idQualification.ToString(),
                        Text = el.name +", "+ el.rang
                    };
                    (ViewData["idQualification"] as List<SelectListItem>).Add(item);
                }
                
                return View(user);
            }
        }
        [HttpPost]
        public ActionResult LogOut(User user)
        {
            Session["User"] = null;
            return RedirectToAction("..");
        }
        [HttpPost]
        public ActionResult Index(User newUser)
        {
            using (AutoCompanyDBEntities ent = new AutoCompanyDBEntities())
            {
                User oldUser = (User)Session["User"];
                newUser.password = oldUser.password;
                newUser.workerCode = oldUser.workerCode;
                List<string> errors = newUser.Validation();
                if (errors == null)
                {
                    if (Models.User.Update(oldUser, newUser))
                    {
                        Session["User"] = newUser;
                    }
                    else
                    {
                        ViewData["ErrorMessage"] = "Что-то пошло не так";
                    }
                }
                else
                {
                    ViewData["ErrorMessage"] = errors;
                    return View();
                }
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Shedule()
        {
            using (AutoCompanyDBEntities ent = new AutoCompanyDBEntities())
            {  
                return View((Session["User"] as driver).GetShedules());
            }
        }
    }
}