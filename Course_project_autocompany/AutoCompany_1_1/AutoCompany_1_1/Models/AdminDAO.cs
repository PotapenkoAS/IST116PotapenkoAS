using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AutoCompany_1_1.Models
{
    public class AdminDAO
    {
        
        static AutoCompanyDBEntities ent = new AutoCompanyDBEntities();

        public static IEnumerable<admin> GetAdmins()
        {
            return from a in ent.admin select a;
        }
    }
}