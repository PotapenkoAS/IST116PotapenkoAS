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
    
    public partial class driver_list
    {
        public int idDriver_list { get; set; }
        public int idDriver { get; set; }
        public int idOrder_list { get; set; }
    
        public virtual driver driver { get; set; }
        public virtual order_list order_list { get; set; }
    }
}
