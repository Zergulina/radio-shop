using OrderService.BLL;
using OrderService.DAL;

namespace OrderService
{
    public static class Startup
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDAL(configuration).AddBLL(configuration);
        }
    }
}
