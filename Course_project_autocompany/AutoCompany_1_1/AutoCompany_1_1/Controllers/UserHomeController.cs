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
            return View();
        }
        [HttpPost]
        public ActionResult Index(Models.User user)
        {
            Session["User"] = null;
            return RedirectToAction("..");
        }
    }
}