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
    
    public partial class driver
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public driver()
        {
            this.bus_selected = new HashSet<bus_selected>();
            this.bus_selected1 = new HashSet<bus_selected>();
            this.bus_selected2 = new HashSet<bus_selected>();
            this.driver_list = new HashSet<driver_list>();
        }
    
        public int idDriver { get; set; }
        public string workerCode { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string surname { get; set; }
        public string name { get; set; }
        public string patronymic { get; set; }
        public string phoneNumber { get; set; }
        public Nullable<int> experience { get; set; }
        public Nullable<int> salary { get; set; }
        public int idQualification { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bus_selected> bus_selected { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bus_selected> bus_selected1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bus_selected> bus_selected2 { get; set; }
        public virtual qualification qualification { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<driver_list> driver_list { get; set; }
    }
}
