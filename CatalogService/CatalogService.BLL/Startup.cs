using System;
using CatalogService.BLL.Interfaces;
using CatalogService.BLL.RabbitMQ.Consumers.Order;
using CatalogService.BLL.RabbitMQ.Consumers.RatingConsumers;
using CatalogService.BLL.Services;
using CatalogService.DAL.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CatalogService.BLL;

public static class Startup
{
    public static IServiceCollection AddBLL(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpcClient<ProductImageGrpc.ProductImageGrpcClient>(options =>
        {
            options.Address = new Uri(configuration.GetConnectionString("ProductImageServiceGrpcConnection"));
        });

        services.AddMassTransit(x =>
        {
            x.AddConsumer<AddOrderConsumer>();
            x.AddConsumer<RemoveOrderConsumer>();

            x.AddConsumer<AddRatingConsumer>();
            x.AddConsumer<RemoveRatingConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("rabbitmq://rabbitmq", h =>
                {
                    h.Username(configuration["RabbitMQ:Username"]);
                    h.Password(configuration["RabbitMQ:Password"]);
                });

                cfg.ReceiveEndpoint("add-order-message-queue", e =>
                {
                    e.ConfigureConsumer<AddOrderConsumer>(context);
                });
                cfg.ReceiveEndpoint("remove-order-message-queue", e =>
                {
                    e.ConfigureConsumer<RemoveOrderConsumer>(context);
                });

                cfg.ReceiveEndpoint("add-rating-message-queue", e =>
                {
                    e.ConfigureConsumer<AddRatingConsumer>(context);
                });
                cfg.ReceiveEndpoint("remove-rating-message-queue", e =>
                {
                    e.ConfigureConsumer<RemoveRatingConsumer>(context);
                });
            });
        });

        services.AddScoped<ITagService, TagService>();
        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}
