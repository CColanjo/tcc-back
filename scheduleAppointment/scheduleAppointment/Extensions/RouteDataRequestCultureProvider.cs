using schedule_appointment_domain.Constants;
using Microsoft.AspNetCore.Localization;

namespace schedule_appointment.Extensions
{
    public class RouteDataRequestCultureProvider : RequestCultureProvider
    {
        public int IndexOfCulture;

        public override Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
        {
            var supportedCultures = GlobalizationConstats.supportedCultures;
            
            ArgumentNullException.ThrowIfNull(httpContext, nameof(httpContext));

            string? uiCulture;
            var culture = uiCulture = httpContext.Request.Path.Value?.Split('/')[IndexOfCulture];

            if (!supportedCultures.Contains(culture))
            {
                culture = "en-US";
            }

            var providerResultCulture = new ProviderCultureResult(culture, uiCulture);

            return Task.FromResult<ProviderCultureResult?>(providerResultCulture);
        }
    }
}
