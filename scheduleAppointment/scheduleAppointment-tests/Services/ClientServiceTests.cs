using Moq;
using schedule_appointment_service.Services;
using schedule_appointment_service.Interface;
using Xunit;
using schedule_appointment_domain.Model.Response;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Default.Project.Api.Controllers;
using static schedule_appointment_domain.Model.ViewModels.ClientViewModel;
using scheduleAppointment_tests.Factories;
using AutoFixture;
using schedule_appointment_domain.Model.Entities;

namespace scheduleAppointment_tests.Services
{
    public class ClientServiceTests
    {
        public readonly ClientServiceFactory _factory;
        public readonly Fixture _fixture;

        public ClientServiceTests()
        {
            _factory = new ClientServiceFactory();
            _fixture = new Fixture();  
        }

        [Fact]
        public async Task Client_GetAllClients_Success()
        {
            var fixtureClient = _fixture.Create<List<Client>>();
            var service = _factory.CreateService();

            var response = await service.GetClients();
            Assert.IsType<List<Client>>(response);
        }
    }
}