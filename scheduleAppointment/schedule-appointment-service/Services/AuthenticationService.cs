
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
using schedule_appointment_domain.Helpers;

namespace schedule_appointment_service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtCredentialsProvider _jwtCredentialsProvider;
        private readonly IUnitOfWork _uow;
        private readonly TokenSettings _setting;

        public AuthenticationService(IUserRepository userRepository, 
            JwtCredentialsProvider jwtCredentialsProvider,
            IUnitOfWork uow,
            IOptions<TokenSettings> setting)
        {
            _userRepository = userRepository;
            _setting = setting.Value;
            _jwtCredentialsProvider = jwtCredentialsProvider;
            _uow = uow; 
        }

        public async Task<TokenResponse> AuthenticateAsync(OAuthRequest authRequest)
        {
           
            var user = await _userRepository.GetByUsernameAsync(authRequest.Username);

            if (user == null)
            {
                throw new JwtClaimException("Usuário ou senha inválidos");
            }

            if (user?.Password != authRequest.Password || !user.Active)
                throw new JwtClaimException("Usuário ou senha inválidos");

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

            var expirationDate = createdDate.Add(new TimeSpan(0, 0, 60));

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
                Username = user.Username,
                Token = jwtHandler.WriteToken(securityToken),
                Created = createdDate,
                Expires = expirationDate,
                RefreshToken = GenerateRefreshAuthenticationToken(user),
                isAdmin = user.IsAdmin
            };
        } 

        public async Task<string> ChangePasswordAsync(OAuthResetPasswordConfirmation request) {

            var user = await _userRepository.GetByUsernameAsync(request.Username);

            if (user is null)
                throw new BusinessException(string.Format("Usuário não encontrado", request.Username));

            if (request.NewPassword != request.NewPasswordConfirmation)
                throw new BusinessException(string.Format("As senhas não são as mesmas", request.OldPassword));

            if (request.OldPassword != user.Password)
                throw new BusinessException(string.Format("A senha está incorreta ou a nova senha é inválida.", request.OldPassword));

            if (!ValidarSenha(request.NewPassword)) {
                throw new BusinessException(string.Format("A senha deve conter caracteres especiais(%¨&amp;@), letras maiúsculas, letras minúscula e ter 10 caracteres", request.OldPassword));
            }

            try {
                user.Password = request.NewPassword;
                _userRepository.Update(user);
                await _uow.Commit();

                return user.Username;
            }
            catch (Exception e)
            {

            }
            return "";
        }

        public bool ValidarSenha(string senha) {
           
            if (senha.Length <= 10) {
                return false;
            }

            // A senha deve conter pelo menos uma letra maiúscula, uma letra minúscula e um caractere especial
            bool contemMaiuscula = false;
            bool contemMinuscula = false;
            bool contemEspecial = false;
            foreach (char c in senha) {
                if (char.IsUpper(c)) {
                    contemMaiuscula = true;
                }
                else if (char.IsLower(c)) {
                    contemMinuscula = true;
                }
                else if (!char.IsLetterOrDigit(c)) {
                    contemEspecial = true;
                }
            }
            if (!contemMaiuscula || !contemMinuscula || !contemEspecial) {
                return false;
            }

            // Se a senha passou por todas as validações, retorna verdadeiro
            return true;
        }

        private string GenerateRefreshAuthenticationToken(User entity)
        {
            var idEntity = new ClaimsIdentity
            (
                new GenericIdentity(entity.Name, ClaimTypes.Sid),
                new[]
                {
                    new System.Security.Claims.Claim("refresh_token", "true"),
                    new System.Security.Claims.Claim("refresh_password", entity.Password)
                }
            );

            var createdDate = DateTime.UtcNow;

            var Seconds = _setting == null? 60 : _setting.Seconds;

            var expirationDate = createdDate.Add(new TimeSpan(0, 0, Seconds * 2));

            var jwtHandler = new JwtSecurityTokenHandler();


            var Issuer = _setting == null ? "" : _setting.Issuer;
            var Audience = _setting == null ? "" : _setting.Audience;
            
            var refreshToken = jwtHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = Issuer,
                Audience = Audience,
                SigningCredentials = _jwtCredentialsProvider.SigningCredentials,
                Subject = idEntity,
                NotBefore = createdDate,
                Expires = expirationDate
            });

            return jwtHandler.WriteToken(refreshToken);
        }
    }
}