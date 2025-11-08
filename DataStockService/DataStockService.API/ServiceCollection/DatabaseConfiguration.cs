using DataStockService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DataStockService.API.ServiceCollection
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection RegisterDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);
        }
    }
}
