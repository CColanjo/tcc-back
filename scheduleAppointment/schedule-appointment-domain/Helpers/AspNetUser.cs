using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace schedule_appointment_domain.Helpers;

public interface IUser
{
    string? Name { get; }
    int GetUserId();
    bool IsAuthenticated();
    bool IsInRole(string role);
    IEnumerable<Claim> GetClaimsIdentity();
}

public class AspNetUser : IUser
{
    private readonly IHttpContextAccessor _accessor;
    public AspNetUser(IHttpContextAccessor accessor) => _accessor = accessor;
    public string? Name => _accessor.HttpContext.User.Identity?.Name;

    public int GetUserId() => IsAuthenticated()
        ? int.Parse(_accessor.HttpContext.User.GetUserId() ??
                     throw new InvalidOperationException("An error occurred while trying to get the user id"))
        : 0;
     
    public bool IsAuthenticated() => _accessor.HttpContext.User.Identity is { IsAuthenticated: true };
    public bool IsInRole(string role) => _accessor.HttpContext.User.IsInRole(role);
    public IEnumerable<Claim> GetClaimsIdentity() => _accessor.HttpContext.User.Claims;
}

public static class ClaimsPrincipalExtensions
{
    public static string? GetUserId(this ClaimsPrincipal principal)
    {
        if (principal is null)
            throw new ArgumentException("Unable to get user id", nameof(principal));

        var claim = principal.FindFirst("userId");

        return claim?.Value;
    }

    public static string? GetUserEmail(this ClaimsPrincipal principal)
    {
        if (principal is null)
            throw new ArgumentException("Unable to get user's email", nameof(principal));

        var claim = principal.FindFirst(ClaimTypes.Email);

        return claim?.Value;
    }
}