using Data;
using Passwords.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Passwords.Services;

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