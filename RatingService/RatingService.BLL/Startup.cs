using MassTransit;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RatingService.BLL.Interfaces;
using RatingService.BLL.Services;

namespace RatingService.BLL
{
    public static class Startup
    {
        public static IServiceCollection AddBLL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddMassTransit(x =>
                {
                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host("rabbitmq://rabbitmq", h =>
                        {
                            h.Username(configuration["RabbitMQ:Username"]);
                            h.Password(configuration["RabbitMQ:Password"]);
                        });

                        cfg.ConfigureEndpoints(context);
                    });
                });
            });

            services.AddGrpcClient<UserGrpc.UserGrpcClient>(options =>
            {
                options.Address = new Uri(configuration.GetConnectionString("UserServiceGrpcConnection"));
            });
            services.AddGrpcClient<OrderGrpc.OrderGrpcClient>(options =>
            {
                options.Address = new Uri(configuration.GetConnectionString("OrderServiceGrpcConnection"));
            });

            services.AddScoped<IProductRatingService, ProductRatingService>();

            return services;
        }
    }
}
