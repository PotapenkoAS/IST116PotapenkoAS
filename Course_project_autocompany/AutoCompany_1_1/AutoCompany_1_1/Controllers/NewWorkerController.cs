using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoCompany_1_1.Controllers
{
    public class NewWorkerController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NewDriver()
        {
            Models.driver driver = new Models.driver();
            driver.workerCode = Models.User.GenerateCode("driver");
            using (Models.AutoCompanyDBEntities ent = new Models.AutoCompanyDBEntities())
            {
                List<Models.qualification> list = (from a in ent.qualification select a).ToList();
                ViewData["idQualification"] = new List<SelectListItem>();
                foreach (Models.qualification el in list)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = el.idQualification.ToString();
                    item.Text = el.name;
                    (ViewData["idQualification"] as List<SelectListItem>).Add(item);
                }
            }
            return View(driver);
        }

        [HttpPost]
        public ActionResult NewDriver(Models.driver driver)
        {
            if (driver.Validation() == null)
            {
                if (Models.User.Find(driver.login) == null)
                {
                    using (Models.AutoCompanyDBEntities ent = new Models.AutoCompanyDBEntities())
                    {
                        ent.driver.Add(driver);
                        ent.SaveChanges();
                        return RedirectToAction("..");

                    }
                }
                else
                {
                    ViewData["ErrorMessage"] = "Пользователь с таким именем уже существует";
                    return View(driver);
                }
            }
            else
            {
                using (Models.AutoCompanyDBEntities ent = new Models.AutoCompanyDBEntities())
                {
                    List<Models.qualification> list = (from a in ent.qualification select a).ToList();
                    ViewData["idQualification"] = new List<SelectListItem>();
                    foreach (Models.qualification el in list)
                    {
                        SelectListItem item = new SelectListItem();
                        item.Value = el.idQualification.ToString();
                        item.Text = el.name;
                        (ViewData["idQualification"] as List<SelectListItem>).Add(item);
                    }
                }
                return View(driver);
            }
        }
        [HttpGet]
        public ActionResult NewAdmin()
        {
            Models.admin admin = new Models.admin();
            admin.workerCode = Models.User.GenerateCode("admin");
            return View(admin);
        }

        [HttpPost]
        public ActionResult NewAdmin(Models.admin admin)
        {

            if (admin.Validation() == null)
            {
                if (Models.User.Find(admin.login) == null)
                {
                    using (Models.AutoCompanyDBEntities ent = new Models.AutoCompanyDBEntities())
                    {
                        ent.admin.Add(admin);
                        ent.SaveChanges();
                        return RedirectToAction("..");
                    }
                }
                else
                {

                    ViewData["ErrorMessage"] = "Пользователь с таким именем уже существует";
                    return View(admin);
                }
            }
            else
            {
                return View(admin);
            }
        }

        [HttpGet]
        public ActionResult NewDispatcher()
        {
            Models.dispatcher dispatcher = new Models.dispatcher();
            dispatcher.workerCode = Models.User.GenerateCode("dispatcher");
            return View(dispatcher);
        }

        [HttpPost]
        public ActionResult NewDispatcher(Models.dispatcher dispatcher)
        {
            if (dispatcher.Validation() == null)
            {
                if (Models.User.Find(dispatcher.login) == null)
                {
                    using (Models.AutoCompanyDBEntities ent = new Models.AutoCompanyDBEntities())
                    {
                        ent.dispatcher.Add(dispatcher);
                        ent.SaveChanges();
                        return RedirectToAction("..");
                    }
                }
                else
                {
                    ViewData["ErrorMessage"] = "Пользователь с таким именем уже существует";
                    return View(dispatcher);
                }
            }
            else
            {
                return View(dispatcher);
            }
        }
    }
}