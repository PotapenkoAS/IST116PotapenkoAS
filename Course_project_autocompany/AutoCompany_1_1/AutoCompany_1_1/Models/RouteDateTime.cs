using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoCompany_1_1.Models
{
    public class RouteDateTime
    {
     
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public RouteDateTime(DateTime ds,DateTime de)
        {
            DateStart = ds;
            DateEnd = de;
        }
    }
}