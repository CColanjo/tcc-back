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
    public class ScheduleServiceFactory {
        public readonly IScheduleRepository _scheduleRepository = Substitute.For<IScheduleRepository>();
        public readonly IUnitOfWork _uow = Substitute.For<IUnitOfWork>();
       

        public ScheduleService CreateService() {
            return new ScheduleService(_scheduleRepository, _uow);
        }
    }
}
