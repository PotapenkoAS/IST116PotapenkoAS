using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoCompany_1_1.Controllers
{
    public class NewWorkerController : Controller
    {
        // GET: NewWorker
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Models.User user)
        {
            string role = user.getRole();
            if (user.Validation() == null)
            {
                using (Models.AutoCompanyDBEntities ent = new Models.AutoCompanyDBEntities())
                {
                    switch (role)
                    {
                        case "admin":
                            {
                                ent.admin.Add(Models.admin.Convert(user));
                                ent.SaveChanges();
                                break;
                            }
                        case "driver":
                            {
                                ent.driver.Add(Models.driver.Convert(user));
                                ent.SaveChanges();
                                break;
                            }
                        case "dispatcher":
                            {
                                ent.dispatcher.Add(Models.dispatcher.Convert(user));
                                ent.SaveChanges();
                                break;
                            }
                        default:
                            break;
                    }
                }
                ViewData["ResultMessage"] = "Success!";
                return View();
            }
            else
            {
                ViewData["ResultMessage"] = "Fail :c";
                return View();
            }
        }

        [HttpGet]
        public ActionResult NewDriver()
        {
            using (Models.AutoCompanyDBEntities ent = new Models.AutoCompanyDBEntities())
            {
                List<Models.qualification> list = (from a in ent.qualification select a).ToList();
                ViewData["idQualification"] = new List<SelectListItem>();
                ViewData["Code"] = Models.User.generateCode("driver");
                foreach (Models.qualification el in list)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = el.idQualification.ToString();
                    item.Text = el.name;
                    (ViewData["idQualification"] as List<SelectListItem>).Add(item);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult NewDriver(Models.driver driver)
        {
            using (Models.AutoCompanyDBEntities ent = new Models.AutoCompanyDBEntities())
            {
                List<Models.qualification> list = (from a in ent.qualification select a).ToList();
                ViewData["idQualification"] = new List<SelectListItem>();
                ViewData["Code"] = Models.User.generateCode("driver");
                foreach (Models.qualification el in list)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = el.idQualification.ToString();
                    item.Text = el.name;
                    (ViewData["idQualification"] as List<SelectListItem>).Add(item);
                }
            }
            return View();
            return RedirectToAction("NewDriver");
        }
    }
}