using NSubstitute;
using schedule_appointment_domain;
using schedule_appointment_domain.Repositories;
using schedule_appointment_service.Interface;
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
       

        public UserService CreateService() {
            return new UserService(_userRepository, _uow);
        }
    }
}
