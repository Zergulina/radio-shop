using CartService.BLL;
using CartService.DAL;

namespace CartService
{
    public static class Startup
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDAL(configuration).AddBLL(configuration);
        }
    }
}
