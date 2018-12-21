using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoCompany_1_1.Models;

namespace AutoCompany_1_1.Controllers
{
    public class TicketController : Controller
    {
        public ActionResult Index()
        {
            using (var ent = new AutoCompanyDBEntities())
            {
                List<route> routeName;
                string query = "select r.* from route r " +
                    "inner join setupped_route sr on sr.idRoute=r.idRoute " +
                    "inner join ticket t on t.idSetupped_route = sr.idSetupped_route ";
                if (Session["User"] != null && (Session["User"] as User).getRole() == "customer")
                {
                    int id = (Session["User"] as customer).idCustomer;
                    var ticks = ent.ticket
                        .Include("seat")
                        .Include("setupped_route")
                        .Include("destination")
                        .Include("destination1")
                        .Include("customer")
                        .Where(t => t.idCustomer == id)
                        .ToList();
                    routeName = ent.Database.SqlQuery<route>(query, "").ToList();
                    ViewData["routeName"] = new List<SelectListItem>();
                    foreach (var el in routeName)
                    {
                        (ViewData["routeName"] as List<SelectListItem>).Add(new SelectListItem() { Value = el.idRoute.ToString(), Text = el.name });
                    }

                    return View(ticks);
                }
                var tickets = ent.ticket
                    .Include("seat")
                    .Include("setupped_route")
                    .Include("destination")
                    .Include("destination1")
                    .Include("customer")
                    .ToList();
                routeName = ent.Database.SqlQuery<route>(query, "").ToList();
                ViewData["routeName"] = new List<SelectListItem>();
                foreach (var el in routeName)
                {
                    (ViewData["routeName"] as List<SelectListItem>).Add(new SelectListItem() { Value = el.idRoute.ToString(), Text = el.name });
                }
                return View(tickets);
            }
        }

        [HttpGet]
        public ActionResult NewTicket()
        {
            DateTime date = DateTime.Now;
            ViewData["date"] = date.Year + "-" + date.Month + "-" + date.Day;
            using (var ent = new AutoCompanyDBEntities())
            {
                var stdest = ent.destination;
                ViewData["startDest"] = new List<SelectListItem>();
                foreach (var el in stdest)
                {
                    (ViewData["startDest"] as List<SelectListItem>)
                        .Add(new SelectListItem() { Text = el.town + ", " + el.name, Value = el.idDestination.ToString() });
                }
                var fdest = ent.destination;
                ViewData["finalDest"] = new List<SelectListItem>();
                foreach (var el in fdest)
                {
                    (ViewData["finalDest"] as List<SelectListItem>)
                        .Add(new SelectListItem() { Text = el.town + ", " + el.name, Value = el.idDestination.ToString() });
                }
                return View();
            }
        }

        [HttpPost]
        public ActionResult NewTicket(DateTime date, ticket ticket)
        {
            using (var ent = new AutoCompanyDBEntities())
            {

                TempData["date"] = date;
                TempData["start"] = ticket.startDest;
                TempData["final"] = ticket.finalDest;
                TempData.Keep();
                return RedirectToAction("TicketSecondStep");
            }
        }

        class IdSR_Name
        {
            public int IdSetupped_route { get; set; }
            public string Name { get; set; }
            public DateTime Time { get; set; }
            public int Cost { get; set; }
        }

        [HttpGet]
        public ActionResult TicketSecondStep()
        {
            using (var ent = new AutoCompanyDBEntities())
            {
                DateTime date = (DateTime)TempData["date"];
                int startDest = (int)TempData["start"];
                int finalDest = (int)TempData["final"];
                TempData.Keep();
                string srouteQuery = "select distinct sr.idSetupped_route,Convert(varchar(max),r.name) as name, sr.DateStart as time, sr.cost  from setupped_route sr " +
                              "inner join route r on r.idRoute=sr.idRoute " +
                              "inner join dest_route dr on dr.idRoute = sr.idRoute " +
                              "inner join destination d on d.idDestination = dr.idDestination " +
                              "where (d.idDestination = @P0 or d.idDestination = @P1) " +
                              "and (Convert(date,sr.dateStart) <=@P2 and Convert(date,sr.dateEnd) >=@P2) ";
                object[] param = new object[] { startDest, finalDest, date.Date };
                List<IdSR_Name> sroute = ent.Database.SqlQuery<IdSR_Name>(srouteQuery, param).ToList();
                ViewData["idSetRoute"] = new List<SelectListItem>();
                ViewData["idSeat"] = new List<SelectListItem>();
                foreach (var el in sroute)
                {
                    (ViewData["idSetRoute"] as List<SelectListItem>).Add(new SelectListItem()
                    { Value = el.IdSetupped_route.ToString(), Text = el.Name + ", " + el.Time + ", " + el.Cost });

                }
                string seatQuery = "select s.* from seat s " +
                    "inner join setupped_route sr on s.idBus=sr.idBus where s.isBusy=1 ";
                var seats = ent.Database.SqlQuery<seat>(seatQuery, new string[0]);
                foreach (var el in seats)
                {
                    (ViewData["idSeat"] as List<SelectListItem>).Add(new SelectListItem()
                    { Value = el.idSeat.ToString(), Text = el.number + ": " + el.column + ", " + el.row });
                }
                return View();
            }
        }
        [HttpPost]
        public ActionResult TicketSecondStep(int idSetRoute, int idSeat, int cost)
        {
            using (var ent = new AutoCompanyDBEntities())
            {
                ticket ticket = new ticket();
                ticket.idCustomer = (Session["User"] as customer).idCustomer;
                ticket.idSeat = idSeat;
                ticket.idSetupped_route = idSetRoute;
                var sroute = ent.setupped_route.Where(sr => sr.idSetupped_route == idSetRoute).FirstOrDefault();
                ticket.idBus = ent.bus.Where(b => b.idBus == sroute.idBus).FirstOrDefault().idBus;
                ticket.cost = cost;
                ticket.date = (DateTime)TempData["date"];
                ticket.startDest = (int)TempData["start"];
                ticket.finalDest = (int)TempData["final"];
                ticket.status = "reserved";
                ent.ticket.Add(ticket);
                ent.SaveChanges();
                return RedirectToAction("Index", "Home", "");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (var ent = new AutoCompanyDBEntities())
            {
               
                List<SelectListItem> list = new List<SelectListItem>()
                {
                    new SelectListItem(){Value="reserved",Text="reserved"},
                    new SelectListItem(){Value="paid",Text="paid"},
                    new SelectListItem(){ Value="canceled",Text="canceled"},
                    new SelectListItem(){Value="done",Text="done"}
                };
                ViewData["status"] = list;
                return View(ent.ticket.Where(t=>t.idTticket==id).FirstOrDefault());
            }
        }

        [HttpPost]
        public ActionResult Edit(ticket ticket)
        {
            using (var ent = new AutoCompanyDBEntities())
            {
                string query = "update ticket set status=@P0 where idTticket=@P1";
                ent.Database.ExecuteSqlCommand(query, new object[] { ticket.status, ticket.idTticket });
                ent.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}