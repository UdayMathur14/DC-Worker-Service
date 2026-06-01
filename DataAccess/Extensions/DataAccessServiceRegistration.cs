using DataAccess.Implementations;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DataAccess.Extensions
{
    public static class DataAccessServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var ilfrmConnectionString = ResolveConnectionString(configuration, ConnectionString.IlfrmSchema, "Oracle_ILFRM");
            var intfConnectionString = ResolveOptionalConnectionString(configuration, ConnectionString.IntfSchema, "Oracle_INTF");

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseOracle(ilfrmConnectionString);
                options.ConfigureWarnings(warnings => warnings.Ignore((EventId)30003));
            });

            if (!string.IsNullOrWhiteSpace(intfConnectionString))
            {
                services.AddDbContext<IntfDbContext>(options =>
                {
                    options.UseOracle(intfConnectionString);
                    options.ConfigureWarnings(warnings => warnings.Ignore((EventId)30003));
                });
            }

            services.AddScoped<IDispatchNoteRepository, DispatchNoteRepository>();

            return services;
        }

        private static string ResolveConnectionString(IConfiguration configuration, string environmentVariableKey, string configurationKey)
        {
            var connectionString =
                configuration.GetConnectionString(configurationKey) ??
                configuration[$"{DataAccess.Configuration.DatabaseConnectionOptions.SectionName}:{configurationKey}"] ??
                Environment.GetEnvironmentVariable(environmentVariableKey);

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    $"Database connection string '{configurationKey}' is missing. Configure appsettings ConnectionStrings:{configurationKey} or set environment variable '{environmentVariableKey}'.");
            }

            return connectionString;
        }

        private static string? ResolveOptionalConnectionString(IConfiguration configuration, string environmentVariableKey, string configurationKey)
        {
            return
                configuration.GetConnectionString(configurationKey) ??
                configuration[$"{DataAccess.Configuration.DatabaseConnectionOptions.SectionName}:{configurationKey}"] ??
                Environment.GetEnvironmentVariable(environmentVariableKey);
        }
    }
}
