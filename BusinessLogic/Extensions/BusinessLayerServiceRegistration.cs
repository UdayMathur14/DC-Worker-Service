using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic.Extensions
{
    public static class BusinessLayerServiceRegistration
    {
        public static IServiceCollection AddBusinessLayerServices(this IServiceCollection services)
        {
            services.AddScoped<IDispatchNoteService, DispatchNoteService>();

            return services;
        }
    }
}
