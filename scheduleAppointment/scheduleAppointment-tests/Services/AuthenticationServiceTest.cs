using AutoFixture;
using NSubstitute;
using schedule_appointment_domain.Model.Request;
using schedule_appointment_domain.Model.Response;
using schedule_appointment_domain.Model.ViewModels;
using scheduleAppointment_tests.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace scheduleAppointment_tests.Services {
    public class AuthenticationServiceTest {

        public readonly AuthenticationServiceFactory _factory;
        public readonly Fixture _fixture;

        public AuthenticationServiceTest() {
            _factory = new AuthenticationServiceFactory();
            _fixture = new Fixture();
        }

        [Fact]
        public async Task User_CreateUser_Success() {
           
            var service = _factory.CreateService();

            var response = await service.AuthenticateAsync(Arg.Any<OAuthRequest>());
            Assert.IsType<TokenResponse>(response);
        }

    }
}
