using CartService.BLL.Interfaces;
using CartService.BLL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CartService.BLL
{
    public static class Startup
    {
        public static IServiceCollection AddBLL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpcClient<UserGrpc.UserGrpcClient>(options =>
            {
                options.Address = new Uri(configuration.GetConnectionString("UserServiceGrpcConnection"));
            });

            services.AddScoped<ICartService, Services.CartService>();

            return services;
        }
    }
}
