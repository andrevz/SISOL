using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SISOL.Infrastructure.Adapters.Repositories;
using SISOL.Infrastructure.Configurations.Persistence.Context;
using SISOL.Infrastructure.Services;

namespace SISOL.Infrastructure;

public static class DependencyInjections
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DbSISOL"));
        });

        services.Scan(p => p
            .FromAssembliesOf(typeof(DepartmentRepository), typeof(UnitOfWork))
            .AddClasses(clases => clases.Where(p => p.Name.EndsWith("Repository") || p.Name.EndsWith("UnitOfWork") || p.Name.EndsWith("Service")))
            .UsingRegistrationStrategy(Scrutor.RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}
