using Microsoft.Extensions.Localization;
using NSubstitute;
using schedule_appointment_domain;
using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.Response;
using schedule_appointment_domain.Repositories;
using schedule_appointment_service.Interface;
using schedule_appointment_service.Localize;
using schedule_appointment_service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static schedule_appointment_domain.Model.ViewModels.ClientViewModel;

namespace scheduleAppointment_tests.Factories {
    public class ClientServiceFactory {
        public readonly IClientRepository _clientRepository = Substitute.For<IClientRepository>();
        public readonly IUnitOfWork _uow = Substitute.For<IUnitOfWork>();
        public ClientServiceFactory GetByIdAsync(Client client) {
            _clientRepository.GetByIdAsync(Arg.Any<int>()).Returns(client);
            return this;

        }

        public ClientServiceFactory GetAllPageableAsync(Page<ClientListViewModel> clientPage) {
            _clientRepository.GetAllPageableAsync(Arg.Any<ClientFindListViewModel>()).Returns(clientPage);
            return this; 
        }

        public ClientServiceFactory GetClients(IEnumerable<ClientResponse> clients) {
            _clientRepository.GetClients().Returns(clients);
            return this; 
        }
        public ClientServiceFactory CreateAsync() {
            _clientRepository.CreateAsync(Arg.Any<Client>());
            return this;
        }


        public ClientService CreateService() {
            return new ClientService(_clientRepository, _uow);
        }
    }
}
