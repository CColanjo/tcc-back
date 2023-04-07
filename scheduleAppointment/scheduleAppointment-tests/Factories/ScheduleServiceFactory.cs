using NSubstitute;
using schedule_appointment_domain;
using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.Response;
using schedule_appointment_domain.Model.ViewModels;
using schedule_appointment_domain.Repositories;
using schedule_appointment_service.Interface;
using schedule_appointment_service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduleAppointment_tests.Factories {
    public class ScheduleServiceFactory {
        public readonly IScheduleRepository _scheduleRepository = Substitute.For<IScheduleRepository>();
        public readonly IUnitOfWork _uow = Substitute.For<IUnitOfWork>();


        public ScheduleServiceFactory GetByIdAsync(Schedule schedule) {
            _scheduleRepository.GetByIdAsync(Arg.Any<int>()).Returns(schedule);
            return this;
        }

        public ScheduleServiceFactory GetAllPageableAsync(Page<ScheduleListViewModel> schedules) {
            _scheduleRepository.GetAllPageableAsync(Arg.Any<ScheduleFindListViewModel>()).Returns(schedules);
            return this;
        }

        public ScheduleServiceFactory GetByDateAsync(IEnumerable<ScheduleResponse> schedules) {
            _scheduleRepository.GetByDateAsync(Arg.Any<DateTime>()).Returns(schedules);
            return this;
        }

        public ScheduleServiceFactory GetAllSchedules(IEnumerable<ScheduleResponse> schedules) {
            _scheduleRepository.GetAllSchedules().Returns(schedules);
            return this;
        }

        public ScheduleServiceFactory GetAllPageableByDateAsync(Page<ScheduleListViewModel> schedules) {
            _scheduleRepository.GetAllPageableByDateAsync(Arg.Any<ScheduleFindListViewModel>(), Arg.Any<DateTime>()).Returns(schedules);
            return this;
        }

        public ScheduleService CreateService() {
            return new ScheduleService(_scheduleRepository, _uow);
        }
    }
}
