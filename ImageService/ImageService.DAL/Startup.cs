using ImageService.DAL.Data;
using ImageService.DAL.Interfaces;
using ImageService.DAL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ImageService.DAL
{
    public static class Startup
    {
        public static IServiceCollection AddDAL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<MinioContext>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();

            return services;
        }

        public static async Task InitializeDataAsync(this IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var minioContext = serviceProvider.GetRequiredService<MinioContext>();

            await minioContext.CreateBucketAsync("ProductImages");
        }
    }
}
