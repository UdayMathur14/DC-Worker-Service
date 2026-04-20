using DataAccess.Implementations;
using DataAccess.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
namespace DataAccess.Extensions
{
    public static class DataAccessServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseOracle(Environment.GetEnvironmentVariable(ConnectionString.IntfSchema));
                options.ConfigureWarnings(warnings => warnings.Ignore((EventId)30003));
            });

            services.AddScoped<IDispatchNoteRepository, DispatchNoteRepository>();



            return services;
        }
    }
}
