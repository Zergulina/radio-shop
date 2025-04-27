using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RadioShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<ProductRating> ProductRatings { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext (DbContextOptions options) : base (options)
        {
            Database.EnsureCreated();
        } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cart>().HasKey(x => new { x.ProductId, x.UserId });
            modelBuilder.Entity<Cart>().HasOne(x => x.Product).WithMany(x => x.Carts).HasForeignKey(x => x.ProductId);
            modelBuilder.Entity<Cart>().HasOne(x => x.User).WithMany(x => x.Carts).HasForeignKey(x => x.UserId);

            modelBuilder.Entity<ProductOrder>().HasKey(x => new { x.ProductId, x.OrderId });
            modelBuilder.Entity<ProductOrder>().HasOne(x => x.Product).WithMany(x => x.ProductOrders).HasForeignKey(x => x.ProductId);
            modelBuilder.Entity<ProductOrder>().HasOne(x => x.Order).WithMany(x => x.ProductOrders).HasForeignKey(x => x.OrderId);

            modelBuilder.Entity<ProductRating>().HasKey(x => new { x.ProductId, x.UserId });
            modelBuilder.Entity<ProductRating>().HasOne(x => x.Product).WithMany(x => x.ProductRatings).HasForeignKey(x => x.ProductId);
            modelBuilder.Entity<ProductRating>().HasOne(x => x.User).WithMany(x => x.ProductRatings).HasForeignKey(x => x.UserId);

            modelBuilder.Entity<Product>().HasMany(x => x.Tags).WithMany(x => x.Products).UsingEntity(x => x.ToTable("ProductTag"));
        }
    }
}
