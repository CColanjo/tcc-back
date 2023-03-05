using AutoMapper;
using FluentValidation.AspNetCore;
using scheduleAppointment.Configuration;
using Serilog;
using schedule_appointment.Extensions;
using schedule_appointment_domain.Constants;
using schedule_appointment_service.Localize;
using schedule_appointment_infra;
using schedule_appointment.Middleware;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Hangfire;
using Hangfire.PostgreSql;

var builder = WebApplication.CreateBuilder(args);

#region Serilog
SerilogExtensions.AddSerilogApi(builder.Configuration);
builder.Host.UseSerilog(Log.Logger);
#endregion

#region Add services to the container.
builder.Services.AddEndpointsApiExplorer();

#region Localization / Culture
builder.Services.AddLocalization();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = GlobalizationConstats.supportedCulturesTyped;

    options.DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders = new IRequestCultureProvider[] { new schedule_appointment.Extensions.RouteDataRequestCultureProvider { IndexOfCulture = 2 } };
});

builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap.Add("culture", typeof(LanguageRouteConstraint));
});
#endregion

#region Configuração da Policy CORS
const string myOrigins = "_origins";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(myOrigins, policyBuilder =>
    {
        policyBuilder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});
#endregion

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddSingleton(provider => new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperConfig(provider.GetService<IStringLocalizer<Resource>>()));
}).CreateMapper());

builder.Services.AddControllers().AddNewtonsoftJson(_ =>
{
    _.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
    _.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    _.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
}).AddFluentValidation(_ => _.RegisterValidatorsFromAssemblyContaining<Program>());

#endregion
//builder.Services.AddHangfire(config =>
//                 config.UsePostgreSqlStorage($"Host=awseb-e-wz4ukimmsc-stack-awsebrdsdatabase-aywomfuzgull.corfxopujuzu.us-east-1.rds.amazonaws.com;Port=5432;Pooling=true;Database=postgres;UserId=postgres;Password=&T1m%142;"));
 

#region Injeção das dependências
InjectorDependency.Injector(builder.Services, builder.Configuration, builder.Environment);
#endregion

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseMiddleware<RequestSerilLogMiddleware>();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlingMiddleware>();

var localizeOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();

if(localizeOptions is not null)
    app.UseRequestLocalization(localizeOptions.Value);

app.UseCors(myOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//app.UseHangfireServer();
//app.UseHangfireDashboard();
app.Run();
