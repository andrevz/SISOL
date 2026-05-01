using Microsoft.EntityFrameworkCore;
using SISOL.Domain.Entities;

namespace SISOL.Infrastructure.Configurations.Persistence.Context;

internal class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Department> Departments => Set<Department>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
