using RatingService.BLL;
using RatingService.DAL;

namespace RatingService
{
    public static class Startup
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDAL(configuration).AddBLL(configuration);
        }
    }
}
