
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using schedule_appointment_domain.Model;
using schedule_appointment_domain.Model.Request;
using schedule_appointment_domain.Model.Response;
using schedule_appointment_service.Interface;

namespace scheduleAppointment.Controllers
{
    [Route("{culture:culture}/authentication")]
    public class AuthenticationController : BaseApiController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(TokenResponse))]
        [ProducesResponseType(401, Type = typeof(RequestResponse))]
        [ProducesResponseType(404, Type = typeof(RequestResponse))]
        [HttpPost("oauth")]
        public async Task<TokenResponse> OAuthAsync([FromBody] OAuthRequest authRequest)
        {
            return await _authenticationService.AuthenticateAsync(authRequest);
        }

        [HttpPut]
        [Route("change-password")]
        public async Task ChangePassword([FromBody] OAuthResetPasswordConfirmation request)
        {
             await _authenticationService.ChangePasswordAsync(request);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("forgot-password")]
        public async Task<string> ForgotPassword([FromQuery] string user)
        {
           return await _authenticationService.ForgotPassword(user);
        }
    }
}
