using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoCompany_1_1.Controllers
{
    public class UserHomeController : Controller
    {
        // GET: UserHome
        [HttpGet]
        public ActionResult Index()
        {
            return View(Session["User"]);
        }
        [HttpPost]
        public ActionResult LogOut(Models.User user)
        {
            Session["User"] = null;
            return RedirectToAction("..");
        }
        [HttpPost]
        public ActionResult Index(Models.User newUser)
        {
            using (Models.AutoCompanyDBEntities ent = new Models.AutoCompanyDBEntities())
            {
                Models.User oldUser = (Models.User)Session["User"];
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
    }
}