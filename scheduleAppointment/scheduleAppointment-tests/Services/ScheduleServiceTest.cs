using AutoFixture;
using NSubstitute;
using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.Response;
using schedule_appointment_domain.Model.ViewModels;
using scheduleAppointment_tests.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
 

namespace scheduleAppointment_tests.Services
{
    public class ScheduleServiceTest {

        public readonly ScheduleServiceFactory _factory;
        public readonly Fixture _fixture;

        public ScheduleServiceTest() {
            _factory = new ScheduleServiceFactory();
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Schedule_GetAllSchedule_Success() {
            var fixtureSchedule = _fixture.Create<List<ScheduleResponse>>();
            var service = _factory.GetAllSchedules(fixtureSchedule).CreateService();

            var response = await service.GetAllSchedules();
            Assert.IsType<List<ScheduleResponse>>(response);
        }

        [Fact]
        public async Task Schedule_Disable_Success() {
            var fixtureSchedule = _fixture.Create<Schedule>();
            var service = _factory.GetByIdAsync(fixtureSchedule).CreateService();

            var response = service.Disable(fixtureSchedule.Id);
            Assert.True(await response);
        }

        [Fact]
        public async Task Schedule_CreateAsync_Success() {

            var fixtureScheduleCreateViewModel = _fixture.Create<ScheduleCreateViewModel>();
            var service = _factory.CreateService();

            var response = await service.CreateAsync(fixtureScheduleCreateViewModel);
            Assert.IsType<int>(response);
        }

         
        [Fact]
        public async Task Schedule_GetByIdAsync_Success() {
            var fixtureSchedule = _fixture.Create<Schedule>();
            var service = _factory.GetByIdAsync(fixtureSchedule).CreateService();

            var response = await service.GetByIdAsync(fixtureSchedule.Id);
            Assert.IsType<ScheduleFindViewModel>(response);
        }

        [Fact]
        public async Task Schedule_GetAllPageableAsync_Success() {
            var fixtureScheduleListViewModelPage = _fixture.Create<Page<ScheduleListViewModel>>();
            var fixtureScheduleFindListViewModel = _fixture.Create<ScheduleFindListViewModel>();
            var service = _factory.GetAllPageableAsync(fixtureScheduleListViewModelPage).CreateService();

            var response = await service.GetAllPageableAsync(fixtureScheduleFindListViewModel);
            Assert.IsType<Page<ScheduleListViewModel>>(response);
        }

        [Fact]
        public async Task Schedule_GetByDateAsync_Success() {
            
            var fixtureDateTime = _fixture.Create<DateTime>();

            var fixtureScheduleResponse = _fixture.Create<List<ScheduleResponse>>();
            
            var service = _factory.GetByDateAsync(fixtureScheduleResponse).CreateService();

            var response = await service.GetByDateAsync(fixtureDateTime);
            Assert.IsType<List<ScheduleResponse>>(response);
        }

        [Fact]
        public async Task Schedule_GetAllPageableByDateAsync_Success() {
            var fixtureScheduleListViewModelPage = _fixture.Create<Page<ScheduleListViewModel>>();
            var fixtureScheduleFindListViewModel = _fixture.Create<ScheduleFindListViewModel>();
            var service = _factory.GetAllPageableByDateAsync(fixtureScheduleListViewModelPage).CreateService();

            var response = await service.GetAllPageableByDateAsync(fixtureScheduleFindListViewModel);
            Assert.IsType<Page<ScheduleListViewModel>>(response);
        }
    }
}
