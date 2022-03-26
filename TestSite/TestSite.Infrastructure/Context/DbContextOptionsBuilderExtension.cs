using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TestSite.Infrastructure.Context
{
    public static class DbContextOptionsBuilderExtension
    {
        public static IServiceCollection AddTestSiteContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TestSiteContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("Default");
                options.UseSqlServer(connectionString);
            });

            return services;
        }
    }
}
