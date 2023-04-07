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

namespace scheduleAppointment_tests.Services
{
    public class ClientsServiceTests
    {
        Mock<IClientService> clienteMock;

        public ClientsServiceTests()
        {
            clienteMock = new Mock<IClientService>();

        }

        [Fact]
        public async Task VerifyGetAllClients()
        {
            var clientsResponse = new List<ClientResponse>() {
                new ClientResponse {Id = 1, Name= "teste 1"},
                new ClientResponse {Id = 2, Name= "teste 2"},
                new ClientResponse {Id = 2, Name= "teste 3"},
            };

            clienteMock.Setup(x => x.GetClients())
               .ReturnsAsync(clientsResponse);

            ClientController clientController = new ClientController(clienteMock.Object);
            IEnumerable<ClientResponse> result = await clientController.GetClients();

            Assert.Equal(3, result.Count());

        }

        [Fact]
        public async Task VerifyGetById()
        {
            var clientsResponse = new ClientFindViewModel { Id = 1 };


            clienteMock.Setup(p => p.GetByIdAsync(1)).ReturnsAsync(clientsResponse);

            ClientController clientController = new ClientController(clienteMock.Object);
            ClientFindViewModel result = await clientController.GetClient(1);

            Assert.Equal(1, result.Id);

        }
    }
}