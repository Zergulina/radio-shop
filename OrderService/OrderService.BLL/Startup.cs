using Google.Protobuf;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.BLL.Interfaces;
using OrderService.BLL.RabbitMQ.Messages;

namespace OrderService.BLL;

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

        services.AddScoped<IOrderService, Services.OrderService>();
        services.AddScoped<IOrderUnitService, Services.OrderUnitService>();

        return services;
    }
}
