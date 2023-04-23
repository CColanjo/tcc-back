
using Microsoft.Extensions.Localization;
using schedule_appointment_domain;
using schedule_appointment_domain.Exceptions;
using schedule_appointment_domain.Helpers;
using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.ViewModels;
using schedule_appointment_domain.Repositories;
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
        private readonly ISendEmail _sendEmail;
   
        public UserService(
            IUserRepository userRepository,
            IUnitOfWork uow,
            ISendEmail sendEmail
            )
        {
            _userRepository = userRepository;
            _uow = uow;
            _sendEmail = sendEmail;
        }

        public async Task<bool> Active(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user is null)
                    throw new Exception();

                user.Active = true;

                _userRepository.Update(user);
                await _uow.Commit();
                return true;
            }
            catch (Exception e)
            {

            }
            return false;
        }

        public async Task<string> CreateAsync(UserCreateViewModel userCreateViewModel)
        {

            var obj = _userRepository.GetByUsernameAsync(userCreateViewModel.Username);
            if (obj.Result != null)
            {
                throw new Exception("Usuário já cadastrado");
            }

            try
            {
              
                var password = PasswordGenerator.GeneratePassword(true, true, true, true, 10);

                var user = new User
                {
                    Username = userCreateViewModel.Username,
                    Password = password,
                    Active = true,
                    CreationDate = DateTime.UtcNow,
                    Name = userCreateViewModel.Name,
                    IsAdmin = userCreateViewModel.IsAdmin,
                    Email = userCreateViewModel.Email
                };

                await _userRepository.CreateAsync(user);
                await _uow.Commit();

            
               _sendEmail.SendEmailAsync(user.Email, "Sua senha é "+ password, user.Name);
                
               return "Criou usuário, verifique o e-mail ou caixa de span";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<bool> Disable(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
                throw new Exception();

            user.Active = false;

            try
            {
                _userRepository.Update(user);
                await _uow.Commit();
                return true;
            }
            catch (Exception e)
            {
                return false;
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

        public async Task<int> Update(UserUpdateViewModel userUpdateViewModel)
        {
            try
            {
                var obj = new User
                {
                    Username = userUpdateViewModel.Username,
                    Active = userUpdateViewModel.Active,
                    Name = userUpdateViewModel.Name,
                    Id = userUpdateViewModel.Id,
                    IsAdmin = userUpdateViewModel.IsAdmin,
                    Email = userUpdateViewModel.Email
                };
                var user = await _userRepository.GetByIdAsync(obj.Id);
                if (user is null)
                    throw new Exception();

                user.Name = obj.Name;
                user.Username = obj.Username;
                user.Active = obj.Active;
                user.IsAdmin = obj.IsAdmin;
                _userRepository.Update(user);

                await _uow.Commit();
                return obj.Id;
            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um erro, aguarde ou entre em contato com o responsável");
            }
        }

        public async Task<Page<UserListViewModel>> GetAllPageableAsync(UserFindListViewModel userPageableRequest)
        {
            var users = await _userRepository.GetAllPageableAsync(userPageableRequest);
            return users;
        }
    }
}
