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
    public class ProfessionalServiceFactory {
        public readonly IProfessionalRepository _professionalRepository = Substitute.For<IProfessionalRepository>();
        public readonly IUnitOfWork _uow = Substitute.For<IUnitOfWork>();
       

        public ProfessionalService CreateService() {
            return new ProfessionalService(_professionalRepository, _uow);
        }
    }
}
