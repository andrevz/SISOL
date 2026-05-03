using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SISOL.Infrastructure.Configurations.Persistence.Context
{
    internal class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var devConnectionString = "Host=localhost;Port=5432;Database=bd_sisol;Username=admin;Password=admin;";
            var connectionString = Environment.GetEnvironmentVariable("DbSISOL") ?? devConnectionString;

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseNpgsql(connectionString, options =>
            {
                options.MigrationsHistoryTable("__EFMigrationHistory", "public");
            });

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
