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
        public ActionResult Edit(Models.User newUser)
        {
            using (Models.AutoCompanyDBEntities ent = new Models.AutoCompanyDBEntities())
            {
                var oldUser = Models.User.Find(((Models.User)Session["User"]).login);
                if(Models.User.Update(oldUser, newUser))
                {
                    Session["User"] = newUser;
                }
                else
                {
                    ViewData["ErrorMessage"] = "Что-то пошло не так";
                }
                return RedirectToAction("Index");
            }
        }
    }
}