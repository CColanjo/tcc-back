using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.ViewModels;

namespace schedule_appointment_domain.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetUserByUsernameAsync(string username); 
        Task<User?> GetByIdAsync(int id); 
        Task<User?> GetByUsernameAsync(string username);
        Task<Page<UserListViewModel>> GetAllPageableAsync(UserFindListViewModel userPageableRequest);
        Task<IEnumerable<UserBarChart>> GetAllUserPerMonth();
    }
}
