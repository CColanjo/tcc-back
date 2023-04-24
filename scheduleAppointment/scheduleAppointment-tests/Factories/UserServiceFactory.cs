using Microsoft.Extensions.Localization;
using NSubstitute;
using schedule_appointment_domain;
using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.ViewModels;
using schedule_appointment_domain.Repositories;
using schedule_appointment_service.Interface;
using schedule_appointment_service.Localize;
using schedule_appointment_service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduleAppointment_tests.Factories {
    public class UserServiceFactory {
        public readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
        public readonly IUnitOfWork _uow = Substitute.For<IUnitOfWork>();
        public readonly ISendEmail _sendEmail = Substitute.For<ISendEmail>();
        public readonly IApikeyRepository _apikeyRepository = Substitute.For<IApikeyRepository>();
        public UserServiceFactory GetAllAsync(IEnumerable<User> users) {
            _userRepository.GetAllAsync().Returns(users);
            return this;
        }
        public UserServiceFactory GetUserByUsernameAsync(User user) {
            _userRepository.GetUserByUsernameAsync(Arg.Any<string>()).Returns(user);
            return this;
        }
        

        public UserServiceFactory GetByIdAsync(User user) {
            _userRepository.GetByIdAsync(Arg.Any<int>()).Returns(user);
            return this;
        }

        public UserServiceFactory GetByUsernameAsync(User user) {
            _userRepository.GetByUsernameAsync(Arg.Any<string>()).Returns(user);
            return this;
        }

        public UserServiceFactory GetAllPageableAsync(Page<UserListViewModel> users) {
            _userRepository.GetAllPageableAsync(Arg.Any<UserFindListViewModel>()).Returns(users);
            return this;
        }

        public UserServiceFactory GetByUserNameAsync(User user) {
            _userRepository.GetByUsernameAsync(Arg.Any<string>()).Returns(user);
            return this;
        }

        public UserServiceFactory GetApiKey(Apikey api)
        {
            _apikeyRepository.GetApikey(Arg.Any<string>()).Returns(api);
            return this;
        } 

        public UserService CreateService() {
            return new UserService(_userRepository, _uow, _sendEmail, _apikeyRepository);
        }
    }
}
