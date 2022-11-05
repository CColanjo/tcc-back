
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Localization;
using schedule_appointment_domain;
using schedule_appointment_domain.Exceptions;
using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.ViewModels;
using schedule_appointment_domain.Repositories;
using schedule_appointment_domain.Security;
using schedule_appointment_service.Interface;
using schedule_appointment_service.Localize;
using schedule_appointment_service.Security;
using System.Net;

namespace schedule_appointment_service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _uow;

        public UserService(
            IUserRepository userRepository,
            IUnitOfWork uow
            ) 
        {
            _userRepository = userRepository;
            _uow = uow;
        }

        public async Task Active(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user is null)
                    throw new Exception();

                user.Active = true;

                _userRepository.Update(user);
                await _uow.Commit();
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }

        public async Task<int> CreateAsync(UserCreateViewModel userCreateViewModel)
        {

            var obj = _userRepository.GetByUsernameAsync(userCreateViewModel.Username);
            if (obj.Result != null)
            {
                throw new NotAuthorizedException("Usuário já cadastrado", HttpStatusCode.Forbidden);
            }

            var user = new User
            {
                Username = userCreateViewModel.Username,
                Password = "1234",
                Active = true,
                CreationDate = DateTime.UtcNow,
                Name = userCreateViewModel.Name,
                IsAdmin = userCreateViewModel.IsAdmin,
            };

            await _userRepository.CreateAsync(user);

            await _uow.Commit();
         
         
            return user.Id;
        }

        public async Task Disable(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user is null)
                    throw new Exception();

                user.Active = false;

                _userRepository.Update(user);
                await _uow.Commit();
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }

        public async Task<UserFindViewModel?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            var obj = new UserFindViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                Active = user.Active,
                IsAdmin = user.IsAdmin
            };

            return obj;
        }

        public async Task Update(UserUpdateViewModel userUpdateViewModel)
        {
            var obj = new User
            {
                Username = userUpdateViewModel.Username,
                Active = userUpdateViewModel.Active,
                Name = userUpdateViewModel.Name,
                Id = userUpdateViewModel.Id,
            };

            try
            {

                var user = await _userRepository.GetByIdAsync(obj.Id);
                if (user is null)
                    throw new Exception();

                user.Name = obj.Name;
                user.Username = obj.Username;
                user.Active = obj.Active;

                _userRepository.Update(user);

                await _uow.Commit();
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }

        public async Task<Page<UserListViewModel>> GetAllPageableAsync(UserFindListViewModel userPageableRequest)
        {
            var users = await _userRepository.GetAllPageableAsync(userPageableRequest);
            return users;
        }
    }
}
