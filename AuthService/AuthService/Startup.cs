using System;
using AuthService.BLL;
using AuthService.DAL;
using Npgsql.Replication;

namespace AuthService;

public static class Startup
{
    public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDLA(configuration).AddBLL(configuration);
    }

    public static async Task InitializeAppAsync(this IServiceProvider serviceProvider, IConfiguration configuration)
    {
        await serviceProvider.InitializeDbAsync(configuration);
    }
}
