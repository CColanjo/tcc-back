using AutoFixture;
using NSubstitute;
using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.Response;
using scheduleAppointment_tests.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static schedule_appointment_domain.Model.ViewModels.ClientViewModel;
using static schedule_appointment_domain.Model.ViewModels.ProfessionalViewModel;

namespace scheduleAppointment_tests.Services {
    public class ProfessionalServiceTest {

        public readonly ProfessionalServiceFactory _factory;
        public readonly Fixture _fixture;

        public ProfessionalServiceTest() {
            _factory = new ProfessionalServiceFactory();
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Professional_GetAll_Success() {
            var fixtureProfessionalResponse = _fixture.Create<List<ProfessionalResponse>>();
            var service = _factory.GetProfessionals(fixtureProfessionalResponse).CreateService();

            var response = await service.GetProfessionals();
            Assert.IsType<List<ProfessionalResponse>>(response);
        }

        [Fact]
        public async Task Professional_GetByIdAsync_Success() {
            var fixtureClient = _fixture.Create<List<ProfessionalResponse>>();
            var service = _factory.GetProfessionals(fixtureClient).CreateService();

            var response = await service.GetByIdAsync(Arg.Any<int>());
            Assert.IsType<ProfessionalFindViewModel>(response);
        }

        [Fact]
        public async Task Professional_GetAllPageableAsync_Success() {
            var fixtureProfessional = _fixture.Create<Page<ProfessionalListViewModel>>();
            var fixtureProfessionalFindListViewModel = _fixture.Create<ProfessionalFindListViewModel>();
            var service = _factory.GetAllPageableAsync(fixtureProfessional).CreateService();

            var response = await service.GetAllPageableAsync(fixtureProfessionalFindListViewModel);
            Assert.IsType<Page<ProfessionalListViewModel>>(response);
        }

        [Fact]
        public async Task Professional_CreateAsync_Success() {

            var fixtureProfessionalFindListViewModel = _fixture.Create<ProfessionalCreateViewModel>();
            var service = _factory.CreateService();

            var response = await service.CreateAsync(fixtureProfessionalFindListViewModel);
            Assert.IsType<int>(response);
        }
         


        [Fact]
        public async Task Professional_Update_Success() {
            var fixture = _fixture.Create<ProfessionalUpdateViewModel>();
            var fixtureProfessional = _fixture.Create<Professional>();
            var service = _factory.GetByIdAsync(fixtureProfessional).CreateService();

            var response = await service.Update(fixture);
            Assert.IsType<int>(response);
        }  

    }
}
