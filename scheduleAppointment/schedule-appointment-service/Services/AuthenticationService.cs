
using schedule_appointment_domain;
using schedule_appointment_domain.Exceptions;
using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Model.Request;
using schedule_appointment_domain.Model.Response;
using schedule_appointment_domain.Repositories;
using schedule_appointment_domain.Security;
using schedule_appointment_domain.Settings;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using schedule_appointment_service.Interface;
using schedule_appointment_service.Localize;
using schedule_appointment_service.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;

namespace schedule_appointment_service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IStringLocalizer<Resource> _localizer;
       
        private readonly TokenSettings _setting;
        private readonly JwtCredentialsProvider _jwtCredentialsProvider;
        private readonly IUnitOfWork _uow;
        private readonly EmailSettings _emailSettings;

        public AuthenticationService(IUserRepository userRepository,
            IStringLocalizer<Resource> localizer,
            IOptions<TokenSettings> setting,
            JwtCredentialsProvider jwtCredentialsProvider,
            IUnitOfWork uow,
            IOptions<EmailSettings> emailSettings)
        {
            _userRepository = userRepository;
            _localizer = localizer;
            _setting = setting.Value;
            _jwtCredentialsProvider = jwtCredentialsProvider;
            _uow = uow;
            _emailSettings = emailSettings.Value;
        }

        public async Task<TokenResponse> AuthenticateAsync(OAuthRequest authRequest)
        {
           
            var user = await _userRepository.GetByUsernameAsync(authRequest.Username);

            if (user == null ||  !user.Active)
            {
                throw new JwtClaimException(_localizer["InvalidUserNameOrPassword"]);
            }

            return GenerateAuthenticationToken(user);
        }

        private TokenResponse GenerateAuthenticationToken(User user)
        {
            var claimsIdentity = new ClaimsIdentity
            (
                new[]
                {
                    new System.Security.Claims.Claim(ClaimTypes.Name, user.Name),
                    new System.Security.Claims.Claim("userId", user.Id.ToString()),
                }
            );

            if (user.IsAdmin) {
                claimsIdentity.AddClaim(new System.Security.Claims.Claim(ClaimTypes.Role, "Admin"));
            }

            var createdDate = DateTime.UtcNow;

            var expirationDate = createdDate.Add(new TimeSpan(0, 0, _setting.Seconds));

            var jwtHandler = new JwtSecurityTokenHandler();

            var securityToken = jwtHandler.CreateToken(new SecurityTokenDescriptor
            {
                SigningCredentials = _jwtCredentialsProvider.SigningCredentials,
                Subject = claimsIdentity,
                NotBefore = createdDate,
                Expires = expirationDate
            });

            return new TokenResponse
            {
                Username = user.Name,
                Token = jwtHandler.WriteToken(securityToken),
                Created = createdDate,
                Expires = expirationDate,
                RefreshToken = GenerateRefreshAuthenticationToken(user),
                isAdmin = user.IsAdmin
            };
        }


        private string GenerateRefreshAuthenticationToken(User entity)
        {
            var idEntity = new ClaimsIdentity
            (
                new GenericIdentity(entity.Name.ToString(), ClaimTypes.Sid),
                new[]
                {
                    new System.Security.Claims.Claim("refresh_token", "true"),
                    new System.Security.Claims.Claim("refresh_password", entity.Password)
                }
            );

            var createdDate = DateTime.UtcNow;

            var expirationDate = createdDate.Add(new TimeSpan(0, 0, _setting.Seconds * 2));

            var jwtHandler = new JwtSecurityTokenHandler();

            var refreshToken = jwtHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _setting.Issuer,
                Audience = _setting.Audience,
                SigningCredentials = _jwtCredentialsProvider.SigningCredentials,
                Subject = idEntity,
                NotBefore = createdDate,
                Expires = expirationDate
            });

            return jwtHandler.WriteToken(refreshToken);
        }

        public async Task<string> ChangePasswordAsync(OAuthResetPasswordConfirmation request)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username);

            if (user is null)
                throw new BusinessException(string.Format(_localizer["UserNotFound"], request.Username));

            if (request.NewPassword != request.NewPasswordConfirmation)
                throw new BusinessException(string.Format(_localizer["PasswordsAreNotTheSame"], request.OldPassword));

            if (request.OldPassword != user.Password)
                throw new BusinessException(string.Format(_localizer["CurrentPasswordDoesNotMatch"], request.OldPassword));

            user.Password = request.NewPassword;

            _userRepository.Update(user);

            return user.Password;
        }
    }
}