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
                if (Models.User.Find(user.login)!=null)
                {
                    Session["User"] = user;
                    ent.customer.Add(customer.Convert(user));
                    ent.SaveChanges();  
                }
                else
                {
                    ViewData["ErrorMessage"] = "Пользователь с таким логином уже существует";
                    return View();
                }
            }
            return RedirectToAction("..");
        }
    }
}