using AutoCompany_1_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoCompany_1_1.Controllers
{
    public class CarController : Controller
    {
        // GET: Car
        public ActionResult Index()
        {

            return View(CarDAO.GetCars());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(car car)
        {
            if (ModelState.IsValid)
            {
                using (AutoCompanyDBEntities ent = new AutoCompanyDBEntities())
                {
                    ent.car.Add(car);
                    ent.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View();
            }
        }
    }
}