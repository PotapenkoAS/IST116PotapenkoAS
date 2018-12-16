using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoCompany_1_1.Models
{
    public class Shedule
    {
        public bus Bus { get; set; }
        public route Route { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public Shedule( bus bus, route route, DateTime timef, DateTime timee)
        {
            Bus = bus;
            Route = route;
            DateStart = timef;
            DateEnd = timee;
        }

    }
}