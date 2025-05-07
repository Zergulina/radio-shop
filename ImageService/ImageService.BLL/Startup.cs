using ImageService.BLL.Interfaces;
using ImageService.BLL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ImageService.BLL
{
    public static class Startup
    {
        public static IServiceCollection AddBLL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductImageService, ProductImageService>();
            
            return services;
        }
    }
}
