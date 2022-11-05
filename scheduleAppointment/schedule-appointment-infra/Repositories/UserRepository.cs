using AutoMapper;
using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.ViewModels;

using schedule_appointment_infra.Extensions;
using Microsoft.EntityFrameworkCore;
using schedule_appointment_domain.Repositories;
using System.Transactions;

namespace schedule_appointment_infra.Repositories;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    private readonly IMapper _mapper;

    public UserRepository(ApplicationDbContext context, IMapper mapper) : base(context)
    {
        _mapper = mapper;

    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.User.AsNoTracking().ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.User.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.User.AsNoTracking().FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _context.User.AsNoTracking().FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<Page<UserListViewModel>> GetAllPageableAsync(UserFindListViewModel userPageableRequest)
    {
        return await _context.User
            .AsNoTracking()
            .PageAsync<User, UserListViewModel>(userPageableRequest, _mapper);
    }

     
}