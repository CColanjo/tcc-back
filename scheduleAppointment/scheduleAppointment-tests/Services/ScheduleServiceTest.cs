using AutoFixture;
using schedule_appointment_domain.Model.Entities;
using scheduleAppointment_tests.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace scheduleAppointment_tests.Services
{
    public class ScheduleServiceTest
    {

        public readonly ScheduleServiceFactory _factory;
        public readonly Fixture _fixture;

        public ScheduleServiceTest() {
            _factory = new ScheduleServiceFactory();
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Schedule_GetAllSchedule_Success() {
            var fixtureClient = _fixture.Create<List<Schedule>>();
            var service = _factory.CreateService();

            var response = await service.GetAllSchedules();
            Assert.IsType<List<Client>>(response);
        } 
    }
}
