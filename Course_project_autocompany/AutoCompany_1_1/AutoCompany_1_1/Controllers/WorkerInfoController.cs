using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoCompany_1_1.Models;

namespace AutoCompany_1_1.Controllers
{
    public class WorkerInfoController : Controller
    {
        // GET: WorkerInfo
        public ActionResult Index(int id)
        {
            using (AutoCompanyDBEntities ent = new AutoCompanyDBEntities())
            {
                driver driver = ent.driver.Include("qualification").Where(d => d.idDriver == id).FirstOrDefault();
                return View(driver);
            }
        }
    }
}