using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.DAL.Data;
using OrderService.DAL.Interfaces;
using OrderService.DAL.Repositories;
using System;

namespace OrderService.DAL;

public static class Startup
{
    public static IServiceCollection AddDAL(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("OrderService"));
        });

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderUnitRepository, OrderUnitRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}
