//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AutoCompany_1_1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ticket
    {
        public int idTticket { get; set; }
        public int idBus { get; set; }
        public int idSeat { get; set; }
        public int idCustomer { get; set; }
        public System.DateTime date { get; set; }
        public int cost { get; set; }
        public int startDest { get; set; }
        public int finalDest { get; set; }
        public int idSetupped_route { get; set; }
        public string status { get; set; }
    
        public virtual customer customer { get; set; }
        public virtual destination destination { get; set; }
        public virtual destination destination1 { get; set; }
        public virtual seat seat { get; set; }
        public virtual setupped_route setupped_route { get; set; }
    }
}
