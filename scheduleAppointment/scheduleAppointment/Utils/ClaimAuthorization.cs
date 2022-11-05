using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using schedule_appointment_domain.Repositories;
using schedule_appointment_domain.Enumerator;

namespace schedule_appointment.Utils
{
    public static class ClaimAuthorization
    {
        //public static bool ValidarClaimsUsuario(HttpContext context, string claim)
        //{
        //    return context.User.Identity is not null && context.User.Identity.IsAuthenticated &&
        //           context.User.Claims.Any(c => c.Type == nameof(claim) && c.Value.Contains(claim));
        //}

        public static bool ValidarClaimsUsuario(HttpContext context, string claim)
        {
            var userRepository = context.RequestServices.GetService<IUserRepository>();

            var idUser = context.User.Claims.FirstOrDefault(u => u.Type == nameof(Claims.userId))?.Value;

            if (string.IsNullOrWhiteSpace(idUser))
                return false;

            var user = userRepository?.GetByIdAsync(int.Parse(idUser)).Result;

            if (user == null)
                return false;

            var claims = "";
            if (user.IsAdmin) {
                claims = "Admin";
            }

            return context.User.Identity is not null &&
                   context.User.Identity.IsAuthenticated &&
                 claims.Contains(claim);
        }
    }

    public class ClaimAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimAuthorizeAttribute(string claim) : base(typeof(RequisitoClaimFilter))
        {
            Arguments = new object[]
            {
                new Claim(claim, claim)
            };
        }
    }

    public class RequisitoClaimFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;
        public RequisitoClaimFilter(Claim claim) => _claim = claim;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User?.Identity is not null && !context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new StatusCodeResult(401);
                return;
            }

            if (!ClaimAuthorization.ValidarClaimsUsuario(context.HttpContext, _claim.Value))
                context.Result = new StatusCodeResult(403);
        }
    }
}