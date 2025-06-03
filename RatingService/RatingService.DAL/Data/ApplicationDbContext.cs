using Microsoft.EntityFrameworkCore;
using RatingService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingService.DAL.Data
{
    internal class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductRating> ProductRatings { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        { 
        
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasMany(x => x.Tags).WithMany(x => x.Products).UsingEntity(x => x.ToTable("ProductTag"));
            modelBuilder.Entity<ProductRating>().HasKey(x => new { x.UserId, x.ProductId });
        }
    }
}
