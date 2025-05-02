using System;
using AuthService.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AuthService.DAL.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public DbSet<User> Users{ get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options) {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
