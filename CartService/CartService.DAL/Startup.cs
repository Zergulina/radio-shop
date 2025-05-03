using CartService.DAL.Data;
using CartService.DAL.Interfaces;
using CartService.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CartService.DAL
{
    public static class Startup
    {
        public static IServiceCollection AddDAL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("CartService"));
            });

            services.AddScoped<ICartRepository, CartRepository>();

            return services;
        }

    }
}
