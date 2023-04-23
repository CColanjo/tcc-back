using AutoFixture;
using Moq;
using NSubstitute;
using schedule_appointment_domain.Exceptions;
using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.Request;
using schedule_appointment_domain.Model.Response;
using schedule_appointment_domain.Model.ViewModels;
using schedule_appointment_service.Interface;
using scheduleAppointment.Controllers;
using scheduleAppointment_tests.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static schedule_appointment_domain.Model.ViewModels.ClientViewModel;

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
        public async Task User_CreateUser_Success() {
            var fixtureUserCreateViewModel = _fixture.Create<UserCreateViewModel>();
            var fixtureApikye = _fixture.Create<Apikey>();
            var service = _factory.GetApiKey(fixtureApikye).CreateService();

            var response = await service.CreateAsync(fixtureUserCreateViewModel);
            Assert.IsType<int>(response);
        }

        
        [Fact]
        public async Task User_CreateUser_UserRegistered() {
            var fixtureUser = _fixture.Create<User>();
            var fixtureUserCreateViewModel = _fixture.Create<UserCreateViewModel>();
            var service = _factory.GetByUserNameAsync(fixtureUser).CreateService();

            var response =  service.CreateAsync(fixtureUserCreateViewModel); 
   
            var exception = Assert.ThrowsAsync<Exception>(() => response);

            Assert.Equal("Usuário já cadastrado", exception.Result.Message);

        }

        [Fact]
        public async Task User_Update_Success() {
            var fixtureUserUpdateViewModel = _fixture.Create<UserUpdateViewModel>();
            var fixtureUser= _fixture.Create<User>();
            var service = _factory.GetByIdAsync(fixtureUser).CreateService();

            var response = await service.Update(fixtureUserUpdateViewModel);
            Assert.IsType<int>(response);
        }

        

        [Fact]
        public async Task User_GetByIdAsync_Success() {
            var fixtureUser = _fixture.Create<User>();
            var service = _factory.GetByIdAsync(fixtureUser).CreateService();

            var response = await service.GetByIdAsync(fixtureUser.Id);
            Assert.IsType<UserFindViewModel>(response);
        }

        [Fact]
        public async Task User_Disable_Success() {
            var fixtureUser = _fixture.Create<User>();
            var service = _factory.GetByIdAsync(fixtureUser).CreateService();

            var response = await service.Disable(fixtureUser.Id);
            Assert.IsType<bool>(response);
        }
        [Fact]
        public async Task User_Active_Success() {
            var fixtureUser = _fixture.Create<User>();
            var service = _factory.GetByIdAsync(fixtureUser).CreateService();

            var response = await service.Active(fixtureUser.Id);
            Assert.IsType<bool>(response);
        }

        [Fact]
        public async Task User_GetAllPageableAsync_Success() {
            var fixtureUserListViewModel = _fixture.Create<Page<UserListViewModel>>();
            var UserFindListViewModel = _fixture.Create<UserFindListViewModel>();
            var service = _factory.GetAllPageableAsync(fixtureUserListViewModel).CreateService();

            var response = await service.GetAllPageableAsync(UserFindListViewModel);
            Assert.IsType<Page<UserListViewModel>>(response);
        }
    }
}
