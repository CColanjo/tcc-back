﻿using AutoFixture;
using schedule_appointment_domain.Model.Entities;
using scheduleAppointment_tests.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

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
            var fixtureClient = _fixture.Create<List<Professional>>();
            var service = _factory.CreateService();

            var response = await service.GetProfessionals();
            Assert.IsType<List<Client>>(response);
        }

    }
}