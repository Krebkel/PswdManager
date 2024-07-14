using DataContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Data;

/// <summary>
/// Зарегистрировать DbContext
/// </summary>
public static class DependencyRegistrations
{
    /// <summary>
    /// Зарегистрировать DbContext
    /// </summary>
    public static IServiceCollection AddPostgres(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>((provider, opt) =>
        {
            var dataOptions = provider.GetRequiredService<IOptions<DataOptions>>().Value;
            if (string.IsNullOrEmpty(dataOptions.ConnectionString))
            {
                throw new InvalidOperationException("ConnectionString is null or empty");
            }
            opt.UseNpgsql(dataOptions.ConnectionString, builder =>
                builder.MigrationsHistoryTable("__EFMigrationsHistory", AppDbContext.ServiceSchema));
        });

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        return services;
    }
}