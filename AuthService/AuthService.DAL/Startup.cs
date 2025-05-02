using System;
using AuthService.DAL.Data;
using AuthService.DAL.Interfaces;
using AuthService.DAL.Models;
using AuthService.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.DAL;

public static class Startup
{
    public static IServiceCollection AddDLA(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("AuthService"));
        });

        services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

        services.Configure<IdentityOptions>(options =>
        {
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
        });

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

    public static async Task InitializeDbAsync(this IServiceProvider serviceProvider, IConfiguration configuration)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

        string[] roles = ["Admin"];

        foreach (var role in roles)
        {
            if (!await roleManager.Roles.AnyAsync(x => x.Name.Equals(role)))
            {
                await roleManager.CreateAsync(new IdentityRole { Name = role, NormalizedName = role.ToUpper() });
            }
        }

        if (!await userManager.Users.AnyAsync(u => u.UserName == configuration["Admin:UserName"]))
        {
            var userName = configuration["Admin:UserName"];
            var password = configuration["Admin:Password"];

            var user = new User()
            {
                UserName = userName,
                Email = configuration["Admin:Email"],
                FirstName =  configuration["Admin:FirstName"],
                LastName =  configuration["Admin:LastName"],
                MiddleName =  configuration["Admin:MiddleName"],
            };

            var createdUser = await userManager.CreateAsync(user, password);
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}
