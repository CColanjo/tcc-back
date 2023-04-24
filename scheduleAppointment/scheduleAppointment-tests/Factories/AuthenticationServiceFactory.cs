using Microsoft.Extensions.Options;
using NSubstitute;
using schedule_appointment_domain;
using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Repositories;
using schedule_appointment_domain.Settings;
using schedule_appointment_service.Interface;
using schedule_appointment_service.Security;
using schedule_appointment_service.Services;

namespace scheduleAppointment_tests.Factories {
    public class AuthenticationServiceFactory {


        private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
        private readonly JwtCredentialsProvider _jwtCredentialsProvider = Substitute.For<JwtCredentialsProvider>();
        private readonly IUnitOfWork _uow = Substitute.For<IUnitOfWork>();
        private readonly IOptions<TokenSettings> _setting = Substitute.For<IOptions<TokenSettings>>();
        private readonly IApikeyRepository _apikeyRepository = Substitute.For<IApikeyRepository>();
        private readonly ISendEmail _sendEmail = Substitute.For<ISendEmail>();

        public AuthenticationServiceFactory GetByUsernameAsync(User user) {
            _userRepository.GetByUsernameAsync(Arg.Any<string>()).Returns(user);
            return this;
        }

        public AuthenticationServiceFactory GetApiKey(Apikey api)
        {
            _apikeyRepository.GetApikey(Arg.Any<string>()).Returns(api);
            return this;
        }

        public AuthenticationService CreateService() {
            return new AuthenticationService(_userRepository, _jwtCredentialsProvider, _uow, _setting, _apikeyRepository, _sendEmail);
        }
    }

}
