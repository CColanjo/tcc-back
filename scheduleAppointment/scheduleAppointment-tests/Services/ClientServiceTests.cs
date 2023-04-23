
using Xunit;
using schedule_appointment_domain.Model.Response;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using static schedule_appointment_domain.Model.ViewModels.ClientViewModel;
using scheduleAppointment_tests.Factories;
using AutoFixture;
using schedule_appointment_domain.Model.Entities;
using NSubstitute;
using schedule_appointment_domain.Model.Pagination;
using System;


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
            var fixtureClient = _fixture.Create<List<ClientResponse>>();
            var service = _factory.GetClients(fixtureClient).CreateService();

            var response =  service.GetClients();
            Assert.IsType<List<ClientResponse>>(await response);
        }

        [Fact]
        public async Task Client_CreateClient_Success() {
            var fixtureClientCreateViewModel = _fixture.Create<ClientCreateViewModel>();
            var service = _factory.CreateAsync().CreateService();

            var response = await service.CreateAsync(fixtureClientCreateViewModel);
            Assert.IsType<int>(response);
        }

        
        [Fact]
        public async Task Client_Update_Success() {
            var fixtureClientUpdateViewModel = _fixture.Create<ClientUpdateViewModel>();
            var fixtureClient = _fixture.Create<Client>();
            var service = _factory.GetByIdAsync(fixtureClient).CreateService();

            var response = await service.Update(fixtureClientUpdateViewModel);
            Assert.IsType<int>(response);
        }

         
        [Fact]
        public async Task Client_GetByIdAsync_Success() {
            var fixtureClient = _fixture.Create<Client>();
            var service = _factory.GetByIdAsync(fixtureClient).CreateService();

            var response = await service.GetByIdAsync(Arg.Any<int>());
            Assert.IsType<ClientFindViewModel>(response);
        }

        [Fact]
        public async Task Client_GetAllPageableAsync_Success() {
            var fixtureClientClientListViewModelPage = _fixture.Create<Page<ClientListViewModel>>();
            var fixtureClientFindListViewModel = _fixture.Create<ClientFindListViewModel>();
            var service = _factory.GetAllPageableAsync(fixtureClientClientListViewModelPage).CreateService();

            var response = await service.GetAllPageableAsync(fixtureClientFindListViewModel);
            Assert.IsType<Page<ClientListViewModel>>(response);
        }  
    }
}