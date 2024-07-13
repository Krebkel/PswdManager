using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Contracts;

namespace Data;

public class AppDbContext : DbContext
{
    private readonly IOptions<DataOptions> _dataOptions;

    public DbSet<PasswordEntry> PasswordEntries { get; set; } = null!; 
 
    public AppDbContext(DbContextOptions<AppDbContext> options, IOptions<DataOptions> dataOptions) : base(options)
    {
        _dataOptions = dataOptions;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.HasDefaultSchema(_dataOptions.Value.ServiceSchema);
    }
}