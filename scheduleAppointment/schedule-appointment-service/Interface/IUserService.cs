﻿
using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.ViewModels;

namespace schedule_appointment_service.Interface
{
    public interface IUserService
    {
        Task<int> CreateAsync(UserCreateViewModel userCreateViewModel);
        Task<int> Update(UserUpdateViewModel userUpdateViewModel);
        Task<UserFindViewModel?> GetByIdAsync(int id);
        Task<bool> Disable(int id);
        Task<bool> Active(int id);
        Task<Page<UserListViewModel>> GetAllPageableAsync(UserFindListViewModel userPageableRequest); 
        Task<IEnumerable<UserBarChart>> GetAllUserPerMonth();
        Task<byte[]> GenerateExcel();
    }
}
