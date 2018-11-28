using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoCompany_1_1.Models
{
    public class CarDAO
    {
        static AutoCompanyDBEntities ent = new AutoCompanyDBEntities();

        public static IEnumerable<car> GetCars()
        { 
            return from a in ent.car select a;
        }
    }
}