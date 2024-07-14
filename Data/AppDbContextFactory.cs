using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Data;

/// <summary>
/// Фабрика DbContext'а для создания миграций.
/// В рантайме не используется, нужна только в момент создания миграций
/// </summary>
internal class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql(
            "Host=***;Port=5432;Database=***;Username=***;Password=***;Pooling=true;Maximum Pool Size=10", 
            builder => builder.MigrationsHistoryTable("__EFMigrationsHistory", AppDbContext.ServiceSchema));

        return new AppDbContext(optionsBuilder.Options);
    }
}