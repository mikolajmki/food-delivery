using food_delivery.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(
            options =>  options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContextConnection")));
        return services;
    }
}
