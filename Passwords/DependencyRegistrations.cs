using Passwords.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Passwords
{
    public static class DependencyRegistrations
    {
        public static IServiceCollection AddPostgresPasswordEntries(this IServiceCollection services)
        {
            services.AddScoped<IPasswordEntryService, PasswordEntryService>();

            return services;
        }
    }
}