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
                Session["User"] = user;
                ent.customer.Add(Models.customer.Convert(user));
                ent.SaveChanges();
            }
                return RedirectToAction("..");
        }
    }
}