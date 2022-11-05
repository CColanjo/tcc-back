using schedule_appointment_domain.Constants;

namespace schedule_appointment.Extensions
{
    public class LanguageRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var supportedCultures = GlobalizationConstats.supportedCultures;

            if (!values.ContainsKey("culture"))
                return false;

            var culture = values["culture"]?.ToString();

            if (!supportedCultures.Contains(culture))
            {
                culture = "en-US";
            }

            return supportedCultures.Contains(culture);
        }
    }
}
