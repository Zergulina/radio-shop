using System;
using CatalogService.BLL.Interfaces;
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

        services.AddScoped<ITagService, TagService>();
        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}
