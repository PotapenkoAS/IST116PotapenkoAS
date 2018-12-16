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
                var user = Models.User.getUserByLP(lp);
                if (user == null)
                {
                    ViewData["ErrorMessage"] = "Неверное имя или пароль";
                    return View();
                }
                else
                {
                    if (user.workerCode == null)
                    {
                        user.workerCode = "";
                    }
                    Session["User"] = user;
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