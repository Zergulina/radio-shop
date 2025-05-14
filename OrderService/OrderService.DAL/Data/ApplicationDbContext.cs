using Microsoft.EntityFrameworkCore;
using OrderService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.DAL.Data
{
    internal class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderUnit> OrderUnits { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasMany(x => x.Tags).WithMany(x => x.Products).UsingEntity(x => x.ToTable("ProductTag"));
            modelBuilder.Entity<OrderUnit>().HasKey(x => new { x.OrderId, x.ProductId });
        }
    }
}
