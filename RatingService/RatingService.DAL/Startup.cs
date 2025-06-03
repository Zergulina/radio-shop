using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RatingService.DAL.Data;
using RatingService.DAL.Interfaces;
using RatingService.DAL.Repositories;

namespace RatingService.DAL
{
    public static class Startup
    {
        public static IServiceCollection AddDAL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("RatingService"));
            });

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductRatingRepository, ProductRatingRepository>();

            services.AddHostedService<DatabaseInitializer>();

            return services;
        }
    }
}
