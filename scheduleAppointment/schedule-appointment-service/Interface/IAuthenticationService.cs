
using schedule_appointment_domain.Model.Request;
using schedule_appointment_domain.Model.Response;

namespace schedule_appointment_service.Interface
{
    public interface IAuthenticationService
    {
        Task<TokenResponse> AuthenticateAsync(OAuthRequest authRequest);

        Task ChangePasswordAsync(OAuthResetPasswordConfirmation request);
    }
}
