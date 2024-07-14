using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Contracts;

namespace Data;

public class AppDbContext : DbContext
{
    internal const string ServiceSchema = "PswdManager";
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}
    
    public DbSet<PasswordEntry> PasswordEntries { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema(ServiceSchema);
        
        ConfigureEntities(builder);
    }

    private static void ConfigureEntities(ModelBuilder builder)
    {
        // Пароли
        builder.Entity<PasswordEntry>();
    }
}