using LeadSync.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LeadSync.Infrastructure
{
  public static class DatabaseMigration
  {
    public static void MigrateDatabase(IServiceProvider services)
    {
      using (var scope = services.CreateScope())
      {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (context.Database.GetPendingMigrations().Any())
        {
          context.Database.Migrate();
        }
      }
    }
  }
}