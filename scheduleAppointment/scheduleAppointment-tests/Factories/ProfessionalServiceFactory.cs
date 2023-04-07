using NSubstitute;
using schedule_appointment_domain;
using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.Response;
using schedule_appointment_domain.Repositories;
using schedule_appointment_infra.Repositories;
using schedule_appointment_service.Interface;
using schedule_appointment_service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static schedule_appointment_domain.Model.ViewModels.ClientViewModel;
using static schedule_appointment_domain.Model.ViewModels.ProfessionalViewModel;

namespace scheduleAppointment_tests.Factories {
    public class ProfessionalServiceFactory {
        public readonly IProfessionalRepository _professionalRepository = Substitute.For<IProfessionalRepository>();
        public readonly IUnitOfWork _uow = Substitute.For<IUnitOfWork>();

        public ProfessionalServiceFactory GetByIdAsync(Professional professional) {
            _professionalRepository.GetByIdAsync(Arg.Any<int>()).Returns(professional);
            return this;
        }

        public ProfessionalServiceFactory GetAllPageableAsync(Page<ProfessionalListViewModel> professionalListViewModel) {
            _professionalRepository.GetAllPageableAsync(Arg.Any<ProfessionalFindListViewModel>()).Returns(professionalListViewModel);
            return this;
        }

        public ProfessionalServiceFactory GetClients(IEnumerable<ProfessionalResponse> professionals) {
            _professionalRepository.GetProfessionals().Returns(professionals);
            return this;

        }

        public ProfessionalService CreateService() {
            return new ProfessionalService(_professionalRepository, _uow);
        }
    }
}
