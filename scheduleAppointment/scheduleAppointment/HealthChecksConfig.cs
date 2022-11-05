using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ischedule_appointment
{
    public static class HealthChecksConfig
    {
        public static IServiceCollection AddHealthCheckConfig(this IServiceCollection services)
        {
            services.AddHealthChecks();

            services.Configure<HealthCheckPublisherOptions>(options =>
            {
                options.Delay = TimeSpan.FromSeconds(5);
                options.Period = TimeSpan.FromSeconds(5);
            });

            return services;
        }
    }
}
