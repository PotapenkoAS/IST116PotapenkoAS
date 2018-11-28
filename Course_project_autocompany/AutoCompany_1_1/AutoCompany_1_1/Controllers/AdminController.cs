using AutoCompany_1_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoCompany_1_1.Controllers
{
    public class AdminController : Controller
    {
        AutoCompanyDBEntities ent = new AutoCompanyDBEntities();
        // GET: Admin
        [HttpGet]
        public ActionResult Index()
        {
            ViewData["admin"] = (from a in ent.admin select a);
            return View(Models.AdminDAO.GetAdmins());
        }
    }
}