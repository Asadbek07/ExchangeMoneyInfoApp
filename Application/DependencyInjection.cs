using Application.Features.SendRecurringInfoFeature;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(cfg =>
                cfg.AddMaps(typeof(SendRecurringInfoProfile).Assembly));
        }
    }
}
