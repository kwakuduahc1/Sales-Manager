using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalesManager.Areas.Stores.Controllers;
using SalesManager.Models;
using System;

namespace SalesManager.Model
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PaymentTypes>(t =>
            {
                t.HasData(new { PaymentType = "Cash", PaymentTypesID = (byte)1 });
                //t.HasData(new { PaymentType = "Mobile Money", PaymentTypesID = (byte)2 });
                //t.HasData(new { PaymentType = "Vodafone Cash", PaymentTypesID = (byte)3 });
            });
            //builder.Entity<Items>(x =>
            //{
            //    x.HasData(new Items { DateAdded = DateTime.UtcNow, Group = "Consoles", ItemName = "Playstation 2", ItemsID = 1, MinimumStock = 20 });
            //    x.HasData(new Items { DateAdded = DateTime.UtcNow, Group = "Consoles", ItemName = "XBox One", ItemsID = 2, MinimumStock = 10 });
            //    x.HasData(new Items { DateAdded = DateTime.UtcNow, Group = "Consoles", ItemName = "XBox 360", ItemsID = 3, MinimumStock = 15 });
            //    x.HasData(new Items { DateAdded = DateTime.UtcNow, Group = "Consoles", ItemName = "XBox", ItemsID = 4, MinimumStock = 5 });
            //    x.HasData(new Items { DateAdded = DateTime.UtcNow, Group = "Contollers", ItemName = "XBox One Wired Controller", ItemsID = 5, MinimumStock = 10 });
            //    x.HasData(new Items { DateAdded = DateTime.UtcNow, Group = "Contollers", ItemName = "XBox 360 Wireless Controller", ItemsID = 6, MinimumStock = 10 });
            //});
            base.OnModelCreating(builder);
        }

        public virtual DbSet<Items> Items { get; set; }

        public virtual DbSet<Stockings> Stockings { get; set; }

        public virtual DbSet<Sales> Sales { get; set; }

        public virtual DbSet<Payments> Payments { get; set; }

        public virtual DbSet<Prices> Prices { get; set; }

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public virtual DbSet<PaymentTypes> PaymentTypes { get; set; }

        public virtual DbSet<Units> Units { get; set; }

        public virtual DbSet<Suppliers> Suppliers { get;  set; }

        public virtual DbSet<SupplierPayments> SupplierPayments { get; set; }
    }
}
