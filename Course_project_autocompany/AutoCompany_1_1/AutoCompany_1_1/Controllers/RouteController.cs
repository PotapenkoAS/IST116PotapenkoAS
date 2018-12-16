using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoCompany_1_1.Controllers
{
    public class RouteController : Controller
    {
        // GET: Route
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Details(int id)//id route
        {
            using (var ent = new Models.AutoCompanyDBEntities())
            {
                List<Models.dest_route> list = ent.dest_route
                    .Include("destination")
                    .Where(dr => dr.idRoute == id).ToList();
                return View(list);
            }
        }
    }
}