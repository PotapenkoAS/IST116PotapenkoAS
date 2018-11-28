using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult Index(Models.User user)
        {
            using (Models.AutoCompanyDBEntities ent = new Models.AutoCompanyDBEntities()) 
            {
                ent.customer.Add(Models.customer.Convert(user));
                ent.SaveChanges();
            }
                return RedirectToAction("..");
        }
    }
}