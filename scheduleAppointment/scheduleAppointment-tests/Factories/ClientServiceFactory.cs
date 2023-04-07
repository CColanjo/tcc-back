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
    public class ClientServiceFactory {
        public readonly IClientRepository _clientRepository = Substitute.For<IClientRepository>();
        public readonly IUnitOfWork _uow = Substitute.For<IUnitOfWork>();
       



        public ClientService CreateService() {
            return new ClientService(_clientRepository, _uow);
        }
    }
}
