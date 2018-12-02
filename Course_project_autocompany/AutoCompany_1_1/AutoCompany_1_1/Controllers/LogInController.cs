using AutoCompany_1_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoCompany_1_1.Controllers
{
    public class LogInController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginPassword lp)
        {
            if (ModelState.IsValid)
            {
                using (AutoCompanyDBEntities ent = new AutoCompanyDBEntities())
                {
                    User user = ent.customer.Where(a => a.login == lp.login && a.password == lp.password).First();
                    if (user != null)
                    {
                        Session["User"] = user;
                    }
                }
                return RedirectToAction("..");
            }
            else
            {
                return View();
            }
           
        }
    }
}