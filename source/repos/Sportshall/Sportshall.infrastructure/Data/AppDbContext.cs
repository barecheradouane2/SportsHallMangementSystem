using Microsoft.EntityFrameworkCore;
using Sportshall.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Activities> Activities { get; set; }

        public virtual DbSet<Attendances> Attendances { get; set; }

        public virtual DbSet<Members> Members { get; set; }

        public virtual DbSet<Offers> Offers { get; set; }

        public virtual DbSet<Payments> Payments { get; set; }   

        public virtual DbSet<Photo> Photo { get; set; }

        public virtual DbSet<Subscriptions> Subscriptions { get; set; }

        public virtual DbSet<Products> Products { get; set; }

        public virtual DbSet<ProductSales> ProductSales { get; set; }

        public virtual DbSet<Revenues>  Revenues { get;set; }

        public virtual DbSet<Expenses> Expenses { get; set; }

        public virtual DbSet<ProductPhoto> ProductPhoto { get; set; }

        public  virtual DbSet<ProductSalesItem> ProductSalesItems { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


           








        }











    }
}
