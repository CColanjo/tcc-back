
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using schedule_appointment_infra_Ioc.Extensions;

namespace schedule_appointment_infra
{
    public static class InjectorDependency
    {
        public static void Injector(IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            services.AddSettings(configuration)
                    .AddConfigureJwt()
                    .AddDbContextConfiguration(configuration) 
                    .AddRepositories()
                    .AddServices()
                    .AddUnitOfWork() 
                    .AddSwaggerConfiguration();
        }
    }
}
