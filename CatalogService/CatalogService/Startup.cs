using System;
using CatalogService.BLL;
using CatalogService.DAL;

namespace CatalogService;

public static class Startup
{
    public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDAL(configuration).AddBLL(configuration);
    }
}
