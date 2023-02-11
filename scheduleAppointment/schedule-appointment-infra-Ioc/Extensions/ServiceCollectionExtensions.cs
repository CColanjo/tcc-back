using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using schedule_appointment_infra;
using Microsoft.EntityFrameworkCore;
using schedule_appointment_domain.Repositories;
using schedule_appointment_infra.Repositories;
using schedule_appointment_service.Interface;
using schedule_appointment_domain.Helpers;
using schedule_appointment_service.Services;
using schedule_appointment_domain.Settings;
using schedule_appointment_service.Security;
using schedule_appointment_domain;

namespace schedule_appointment_infra_Ioc.Extensions
{
    public static class ServiceCollectionExtensions
    {
       

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
          
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<IUser, AspNetUser>();

            return services;
        }

        public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionStringsSettings>(configuration.GetSection("ConnectionStrings"));
            services.Configure<TokenSettings>(configuration.GetSection("AuthorizeConfig"));
            
            return services;
        }

        public static IServiceCollection AddConfigureJwt(this IServiceCollection services)
        {
            var path = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Cyphers\\cypher.pem");


            var jwtCredentialsProvider = new JwtCredentialsProvider(path);
            services.AddSingleton(jwtCredentialsProvider);

            services.AddAuthentication(authOptions =>
                {
                    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(bearerOptions =>
                {
                    var paramsValidation = bearerOptions.TokenValidationParameters;
                    paramsValidation.IssuerSigningKey = jwtCredentialsProvider.Key;
                    paramsValidation.ValidateAudience = false;
                    paramsValidation.ValidateIssuer = false;
                    paramsValidation.ValidateIssuerSigningKey = true;
                    paramsValidation.ClockSkew = TimeSpan.Zero;
                });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            return services;
        }

     

        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }

        public static IServiceCollection AddDbContextConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            var url_database = Environment.GetEnvironmentVariable("URL_DATABASE");
            var db_database = Environment.GetEnvironmentVariable("DB_DATABASE");
            var user_database = Environment.GetEnvironmentVariable("USER_DATABASE");
            var password_database = Environment.GetEnvironmentVariable("DB_PASS");

            string connectionString = string.Empty;
            connectionString = $"Host=awseb-e-wz4ukimmsc-stack-awsebrdsdatabase-aywomfuzgull.corfxopujuzu.us-east-1.rds.amazonaws.com;Port=5432;Pooling=true;Database=postgres;UserId=postgres;Password=&T1m%142;";

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString,
                        npgSqlBuilder => npgSqlBuilder
                            .EnableRetryOnFailure(3, TimeSpan.FromSeconds(3), null))
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors());

            return services;
        }

        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
