using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SISOL.Application.Common.Contracts.Repositories;
using SISOL.Infrastructure.Adapters.Repositories;
using SISOL.Infrastructure.Configurations.Persistence.Context;

namespace SISOL.Infrastructure;

public static class DependencyInjections
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DbSISOL"));
        });

        services.AddScoped<IDepartmentRepository, DepartmentRepository>();

        return services;
    }
}
