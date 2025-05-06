using CatalogService.DAL.Data;
using CatalogService.DAL.Interfaces;
using CatalogService.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.DAL;

public static class Startup
{
    public static IServiceCollection AddDAL(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => 
        {
           options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("CatalogService"));
        });

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IProductTagRepository, ProductTagRepository>();

        services.AddHostedService<DatabaseInitializer>();

        return services;
    }
}
