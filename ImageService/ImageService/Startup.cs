using ImageService.BLL;
using ImageService.DAL;

namespace ImageService
{
    public static class Startup
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDAL(configuration).AddBLL(configuration);
            return services;
        }
    }
}

using ImageService.BLL;
using ImageService.DAL;

namespace ImageService
{
    public static class Startup
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDAL(configuration).AddBLL(configuration);
        }

        public static async Task InitializeAppAsync(this IServiceProvider serviceProvider, IConfiguration configuration)
        {
            await serviceProvider.InitializeDataAsync(configuration);
        }
    }
}
