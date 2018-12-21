using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoCompany_1_1.Models;

namespace AutoCompany_1_1.Controllers
{
    public class SetupDriverController : Controller
    {
        private void GenLists(setupped_route bs)
        {
            using (AutoCompanyDBEntities ent = new AutoCompanyDBEntities())
            {
                List<route> routeList = (from a in ent.route select a).ToList();
                ViewData["idRoute"] = new List<SelectListItem>();
                foreach (route el in routeList)
                {
                    SelectListItem item = new SelectListItem
                    {
                        Value = el.idRoute.ToString(),
                        Text = el.name
                    };
                    if (bs != null && el.idRoute == bs.idRoute)
                    {
                        item.Selected = true;
                    }
                    (ViewData["idRoute"] as List<SelectListItem>).Add(item);
                }

                string query = "select d.* from driver d inner join qualification q on d.idQualification = q.idQualification where q.name=@P0";
                List<driver> driverlist1 = ent.Database.SqlQuery<driver>(query, new object[1] { "bus driver" }).ToList();
                ViewData["idFirstDriver"] = new List<SelectListItem>();
                foreach (driver el in driverlist1)
                {
                    SelectListItem item = new SelectListItem
                    {
                        Value = el.idDriver.ToString(),
                        Text = el.name
                    };
                    if (bs != null && el.idDriver == bs.idFirstDriver)
                    {
                        item.Selected = true;
                    }
                    (ViewData["idFirstDriver"] as List<SelectListItem>).Add(item);
                }

                List<driver> driverlist2 = ent.Database.SqlQuery<driver>(query, new object[1] { "bus driver" }).ToList();
                ViewData["idSecondDriver"] = new List<SelectListItem>();
                (ViewData["idSecondDriver"] as List<SelectListItem>).Add(new SelectListItem() { Value = null, Text = "" });
                foreach (driver el in driverlist2)
                {
                    SelectListItem item = new SelectListItem
                    {
                        Value = el.idDriver.ToString(),
                        Text = el.name
                    };
                    if (bs != null && el.idDriver == bs.idSecondDriver)
                    {
                        item.Selected = true;
                    }
                    (ViewData["idSecondDriver"] as List<SelectListItem>).Add(item);
                }

                List<driver> conductorList = ent.Database.SqlQuery<driver>(query, new object[1] { "conductor" }).ToList();
                ViewData["idConductor"] = new List<SelectListItem>();
                (ViewData["idConductor"] as List<SelectListItem>).Add(new SelectListItem() { Value = null, Text = "" });
                foreach (driver el in conductorList)
                {
                    SelectListItem item = new SelectListItem
                    {
                        Value = el.idDriver.ToString(),
                        Text = el.name
                    };
                    if (bs != null && el.idDriver == bs.idConductor)
                    {
                        item.Selected = true;
                    }
                    (ViewData["idConductor"] as List<SelectListItem>).Add(item);
                }

                List<bus> busList = (from a in ent.bus select a).ToList();
                ViewData["idBus"] = new List<SelectListItem>();
                foreach (bus el in busList)
                {
                    SelectListItem item = new SelectListItem
                    {
                        Value = el.idBus.ToString(),
                        Text = el.name
                    };
                    if (bs != null && el.idBus == bs.idBus)
                    {
                        item.Selected = true;
                    }
                    (ViewData["idBus"] as List<SelectListItem>).Add(item);
                }
            }
        }

        private bool IsFree(driver dr)
        {
            var sr = Session["sr"] as setupped_route;
            bool flag = true;
            TimeSpan srSpan = sr.dateEnd - sr.dateStart;
            List<Shedule> slist = dr.GetShedules();

            if (slist.Count == 1)
            {
                if (sr.dateStart > slist.ElementAt(0).DateEnd || sr.dateEnd < slist.ElementAt(0).DateStart)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            for (int i = 0; i < slist.Count - 1; i++)
            {


                if (sr.dateStart > slist.ElementAt(i).DateEnd && sr.dateEnd < slist.ElementAt(i + 1).DateStart)
                {
                    flag = false;
                    TimeSpan shSpan = slist.ElementAt(i + 1).DateStart - slist.ElementAt(i).DateEnd;
                    if (shSpan.Hours < srSpan.Hours)
                    {
                        return true;
                    }
                }
                else if (sr.dateStart < slist.ElementAt(i).DateEnd && sr.dateEnd > slist.ElementAt(i + 1).DateStart)
                {
                    return false;
                }


            }
            return flag;
        }

        [HttpGet]
        public ActionResult Index()
        {
            using (AutoCompanyDBEntities ent = new AutoCompanyDBEntities())
            {

                DateTime date = DateTime.Now.Date;
                ViewData["date"] = date.Year + "-" + date.Month + "-" + date.Day;
                List<setupped_route> list = ent.setupped_route
               .Include("bus")
               .Include("route")
               .Include("firstDriver")
               .Include("secondDriver")
               .Include("conductor")
               .Where(a => DbFunctions.TruncateTime(a.dateStart) <= date.Date && DbFunctions.TruncateTime(a.dateEnd) >= date.Date)
               .OrderBy(a => a.dateStart)
               .ToList();
                return View(list);
            }
        }

        [HttpPost]
        public ActionResult Index(DateTime date)
        {
            using (AutoCompanyDBEntities ent = new AutoCompanyDBEntities())
            {
                if (date == null)
                {
                    date = DateTime.Now.Date;
                }
                ViewData["date"] = date.Year + "-" + date.Month + "-" + date.Day;
                List<setupped_route> list = ent.setupped_route
                .Include("bus")
                .Include("route")
                .Include("firstDriver")
                .Include("secondDriver")
                .Include("conductor")
                .Where(a => DbFunctions.TruncateTime(a.dateStart) <= date.Date && DbFunctions.TruncateTime(a.dateEnd) >= date.Date)
                .OrderBy(a => a.dateStart)
                .ToList();
                return View(list);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            GenLists(null);
            DateTime date = DateTime.Now.Date;
            ViewData["date"] = date.Year + "-" + date.Month + "-" + date.Day;
            return View();

        }

        [HttpPost]
        public ActionResult Create(setupped_route sr)
        {
            using (AutoCompanyDBEntities ent = new AutoCompanyDBEntities())
            {
                List<dest_route> dr = ent.dest_route.Where(d => d.idRoute == sr.idRoute).ToList();
                var dateFrom = dr.FirstOrDefault().timeArrive;
                var dateTo = dr.LastOrDefault().timeOut;
                sr.dateStart = (DateTime)(sr.dateStart + dateFrom);
                sr.dateEnd = (DateTime)(sr.dateStart + (dateTo - dateFrom));
                Session["sr"] = sr;
                return RedirectToAction("CreateSecondStep");
            }
        }

        [HttpGet]
        public ActionResult CreateSecondStep()
        {
            using (AutoCompanyDBEntities ent = new AutoCompanyDBEntities())
            {
                string query = "select d.* from driver d inner join qualification q on d.idQualification = q.idQualification where q.name=@P0";
                List<driver> driverlist1 = ent.Database.SqlQuery<driver>(query, new object[] { "bus driver" }).ToList();
                ViewData["idFirstDriver"] = new List<SelectListItem>();
                foreach (driver el in driverlist1)
                {

                    if (IsFree(el))
                    {
                        SelectListItem item = new SelectListItem
                        {
                            Value = el.idDriver.ToString(),
                            Text = el.name
                        };
                        (ViewData["idFirstDriver"] as List<SelectListItem>).Add(item);
                    }
                }


                ViewData["idSecondDriver"] = new List<SelectListItem>();
                foreach (driver el in driverlist1)
                {
                    if (IsFree(el))
                    {
                        SelectListItem item = new SelectListItem
                        {
                            Value = el.idDriver.ToString(),
                            Text = el.name
                        };
                        (ViewData["idSecondDriver"] as List<SelectListItem>).Add(item);
                    }
                }
                (ViewData["idSecondDriver"] as List<SelectListItem>).Add(new SelectListItem() { Value = null, Text = null });

                List<driver> conductorList = ent.Database.SqlQuery<driver>(query, new object[1] { "conductor" }).ToList();
                ViewData["idConductor"] = new List<SelectListItem>();
                foreach (driver el in conductorList)
                {
                    if (IsFree(el))
                    {
                        SelectListItem item = new SelectListItem
                        {
                            Value = el.idDriver.ToString(),
                            Text = el.name
                        };
                        (ViewData["idConductor"] as List<SelectListItem>).Add(item);
                    }
                }
                 (ViewData["idConductor"] as List<SelectListItem>).Add(new SelectListItem() { Value = null, Text = null });
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateSecondStep(int? idFirstDriver, int? idSecondDriver, int? idConductor)
        {
            if (idFirstDriver == null)
            {
                ViewData["Errors"] = new List<string>() { "First driver have to be setupped" };
                return View();
            }
            using (var ent = new AutoCompanyDBEntities())
            {
                var sr = Session["sr"] as setupped_route;
                Session["sr"] = null;
                sr.idFirstDriver = (int)idFirstDriver;
                sr.idSecondDriver = idSecondDriver;
                sr.idConductor = idConductor;
                string query = "insert into setupped_route (idRoute,idBus,dateStart,dateEnd,idFirstDriver,idSecondDriver,idConductor) values(@P0,@P1,@P2,@P3,@P4,@P5,@P6)";
                object[] parameters = new object[]
                {
                    sr.idRoute,
                    sr.idBus,
                    sr.dateStart,
                    sr.dateEnd,
                    sr.idFirstDriver,
                    sr.idSecondDriver,
                    sr.idConductor
                };
                ent.Database.ExecuteSqlCommand(query, parameters);
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public ActionResult Edit(int id_bs)
        {
            using (AutoCompanyDBEntities ent = new AutoCompanyDBEntities())
            {
                setupped_route bs = ent.setupped_route
                 .Include("bus")
                 .Include("route")
                 .Include("firstDriver")
                 .Include("secondDriver")
                 .Include("conductor")
                 .Where(a => a.idSetupped_route == id_bs)
                 .FirstOrDefault();


                GenLists(bs);
                return View(bs);
            }
        }
        [HttpPost]
        public ActionResult Edit(setupped_route bs)
        {
            using (AutoCompanyDBEntities ent = new AutoCompanyDBEntities())
            {
                string query = "update setupped_route set " +
                    "idRoute=@P0, " +
                    "idBus=@P1, " +
                    "dateStart=@P2, " +
                    "idFirstDriver=@P3, " +
                    "idSecondDriver=@P4, " +
                    "idConductor=@P5 " +
                    "where idSetupped_route =@P6";
                object[] par = new object[]
                {
                    bs.idRoute,
                    bs.idBus,
                    bs.dateStart,
                    bs.idFirstDriver,
                    bs.idSecondDriver,
                    bs.idConductor,
                    bs.idSetupped_route
                };
                ent.Database.ExecuteSqlCommand(query, par);
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (var ent = new AutoCompanyDBEntities())
            {
                var sr = ent.setupped_route
                     .Include("bus")
                     .Include("route")
                     .Include("firstDriver")
                     .Include("secondDriver")
                     .Include("conductor")
                     .Where(s => s.idSetupped_route == id).FirstOrDefault();
                return View(sr);
            }
        }
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            using (var ent = new AutoCompanyDBEntities())
            {
                var sr = ent.setupped_route.Where(s => s.idSetupped_route == id).FirstOrDefault();
                ent.setupped_route.Remove(sr);
                ent.SaveChanges();
                return RedirectToAction("Index");
            }

        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            using (var ent = new AutoCompanyDBEntities())
            {
                var sr = ent.setupped_route
                     .Include("bus")
                     .Include("route")
                     .Include("firstDriver")
                     .Include("secondDriver")
                     .Include("conductor")
                     .Where(s => s.idSetupped_route == id).FirstOrDefault();
                return View(sr);
            }
        }
    }
}
