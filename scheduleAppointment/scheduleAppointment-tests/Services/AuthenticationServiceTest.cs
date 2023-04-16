using AutoFixture;
using Microsoft.Extensions.Localization;
using NSubstitute;
using schedule_appointment_domain;
using schedule_appointment_domain.Exceptions;
using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Model.Request;
using schedule_appointment_domain.Model.Response;
using schedule_appointment_domain.Repositories;
using schedule_appointment_service.Localize;
using scheduleAppointment_tests.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace scheduleAppointment_tests.Services {
    public class AuthenticationServiceTest {
        public readonly AuthenticationServiceFactory  _factory;
        public readonly Fixture _fixture;
    
        public AuthenticationServiceTest() {
            _factory = new AuthenticationServiceFactory();
            _fixture = new Fixture();
            
        }

        [Fact]
        public async Task Authentication_Success() {
            var fixture = _fixture.Create<User>();
            var fixtureOAuthRequest = _fixture.Create<OAuthRequest>();
            fixture.Password = fixtureOAuthRequest.Password;
            var service = _factory.GetByUsernameAsync(fixture).CreateService();

            var response = service.AuthenticateAsync(fixtureOAuthRequest);
            Assert.IsType<TokenResponse>(await response);
        }

        [Fact]
        public async Task Authentication_Error_UrerNotFound() {
            var fixture = _fixture.Create<User>();
            var fixtureOAuthRequest = _fixture.Create<OAuthRequest>();
            var service = _factory.CreateService();

            var response = service.AuthenticateAsync(fixtureOAuthRequest);
            var exception = Assert.ThrowsAsync<JwtClaimException>(() => response);

            Assert.Equal("Usuário ou senha inválidos", exception.Result.Message);
        }

        [Fact]
        public async Task Authentication_Error_PassWordWrong() {
            var fixture = _fixture.Create<User>();
            var fixtureOAuthRequest = _fixture.Create<OAuthRequest>();
  
            var service = _factory.GetByUsernameAsync(fixture).CreateService();

            var response = service.AuthenticateAsync(fixtureOAuthRequest); 
            var exception = Assert.ThrowsAsync<JwtClaimException>(() => response);

            Assert.Equal("Usuário ou senha inválidos", exception.Result.Message);
        }

        [Fact]
        public async Task Authentication_ChangePassword_Success() {
            var fixture = _fixture.Create<User>();
            var fixtureOAuthResetPasswordConfirmation = _fixture.Create<OAuthResetPasswordConfirmation>();
            fixtureOAuthResetPasswordConfirmation.NewPassword = fixtureOAuthResetPasswordConfirmation.NewPasswordConfirmation;
            fixture.Password = fixtureOAuthResetPasswordConfirmation.OldPassword;
            var service = _factory.GetByUsernameAsync(fixture).CreateService();

            var response = service.ChangePasswordAsync(fixtureOAuthResetPasswordConfirmation);
            Assert.IsType<string>(await response);
        }

        [Fact]
        public async Task Authentication_ChangePassword_UserNotFound() {
            var fixture = _fixture.Create<User>();
        
            var fixtureOAuthResetPasswordConfirmation = _fixture.Create<OAuthResetPasswordConfirmation>();
            var service = _factory.CreateService();

            var response =  service.ChangePasswordAsync(fixtureOAuthResetPasswordConfirmation);
            var exception = Assert.ThrowsAsync<BusinessException>(() => response);

            Assert.Contains("Usuário não encontrado",  exception.Result.Message);
        }


        [Fact]
        public async Task Authentication_ChangePassword_PasswordsAreNotTheSame() {
            var fixture = _fixture.Create<User>();

            var fixtureOAuthResetPasswordConfirmation = _fixture.Create<OAuthResetPasswordConfirmation>();
            var service = _factory.GetByUsernameAsync(fixture).CreateService();

            var response = service.ChangePasswordAsync(fixtureOAuthResetPasswordConfirmation);
            var exception = Assert.ThrowsAsync<BusinessException>(() => response);

            Assert.Contains("As senhas não são as mesmas", exception.Result.Message);
        }

        [Fact]
        public async Task Authentication_ChangePassword_CurrentPasswordDoesNotMatch() {
            var fixture = _fixture.Create<User>();
            var fixtureOAuthResetPasswordConfirmation = _fixture.Create<OAuthResetPasswordConfirmation>();
            fixtureOAuthResetPasswordConfirmation.NewPassword = fixtureOAuthResetPasswordConfirmation.NewPasswordConfirmation;
            var service = _factory.GetByUsernameAsync(fixture).CreateService();

            var response = service.ChangePasswordAsync(fixtureOAuthResetPasswordConfirmation);
            var exception = Assert.ThrowsAsync<BusinessException>(() => response);

            Assert.Contains("A senha está incorreta ou a nova senha é inválida", exception.Result.Message);
        }

        [Fact]
        public async Task Authentication_ChangePassword_PasswordWrong() {
            var fixture = _fixture.Create<User>();
            var fixtureOAuthResetPasswordConfirmation = _fixture.Create<OAuthResetPasswordConfirmation>();
            fixtureOAuthResetPasswordConfirmation.NewPassword = "12345";
            fixtureOAuthResetPasswordConfirmation.NewPasswordConfirmation = "12345";
            fixtureOAuthResetPasswordConfirmation.OldPassword = fixture.Password;
            var service = _factory.GetByUsernameAsync(fixture).CreateService();

            var response = service.ChangePasswordAsync(fixtureOAuthResetPasswordConfirmation);
            var exception = Assert.ThrowsAsync<BusinessException>(() => response);
            Assert.Contains("A senha deve conter caracteres especiais(%¨&amp;@), letras maiúsculas, letras minúscula e ter 10 caracteres", exception.Result.Message);
        }
    }
}
