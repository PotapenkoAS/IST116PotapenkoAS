﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AutoCompanyDBEntities : DbContext
    {
        public AutoCompanyDBEntities()
            : base("name=AutoCompanyDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<admin> admin { get; set; }
        public virtual DbSet<bus> bus { get; set; }
        public virtual DbSet<bus_selected> bus_selected { get; set; }
        public virtual DbSet<car> car { get; set; }
        public virtual DbSet<car_list> car_list { get; set; }
        public virtual DbSet<car_type> car_type { get; set; }
        public virtual DbSet<car_type_qual> car_type_qual { get; set; }
        public virtual DbSet<customer> customer { get; set; }
        public virtual DbSet<dest_route> dest_route { get; set; }
        public virtual DbSet<destination> destination { get; set; }
        public virtual DbSet<dispatcher> dispatcher { get; set; }
        public virtual DbSet<driver> driver { get; set; }
        public virtual DbSet<driver_list> driver_list { get; set; }
        public virtual DbSet<order> order { get; set; }
        public virtual DbSet<order_list> order_list { get; set; }
        public virtual DbSet<qualification> qualification { get; set; }
        public virtual DbSet<route> route { get; set; }
        public virtual DbSet<seat> seat { get; set; }
        public virtual DbSet<service> service { get; set; }
        public virtual DbSet<service_car_type> service_car_type { get; set; }
        public virtual DbSet<ticket> ticket { get; set; }
    }
}
