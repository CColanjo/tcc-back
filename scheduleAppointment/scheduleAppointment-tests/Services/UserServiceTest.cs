using AutoFixture;
using Moq;
using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Model.Request;
using schedule_appointment_domain.Model.Response;
using schedule_appointment_service.Interface;
using scheduleAppointment.Controllers;
using scheduleAppointment_tests.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace scheduleAppointment_tests.Services
{
    public class UserServiceTest {

        public readonly UserServiceFactory _factory;
        public readonly Fixture _fixture;

        public UserServiceTest() {
            _factory = new UserServiceFactory();
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Client_GetAllClients_Success() {
            var fixtureClient = _fixture.Create<List<User>>();
            var service = _factory.CreateService(); 
        } 
    }
}
