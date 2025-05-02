using System;
using CatalogService.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.DAL.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products{ get; set; }
    public DbSet<Tag> Tags{ get; set; }
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().HasMany(x => x.Tags).WithMany(x => x.Products).UsingEntity(x => x.ToTable("ProductTag"));
    }
}
