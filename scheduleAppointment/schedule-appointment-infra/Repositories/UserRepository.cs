using AutoMapper;
using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.ViewModels;

using schedule_appointment_infra.Extensions;
using Microsoft.EntityFrameworkCore;
using schedule_appointment_domain.Repositories;
using System.Transactions;
using static schedule_appointment_domain.Model.ViewModels.ClientViewModel;
using System.Text.RegularExpressions;
using System.Globalization;

namespace schedule_appointment_infra.Repositories;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    private readonly IMapper _mapper;
    private CultureInfo culture = new CultureInfo("pt-BR");
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


    public async Task<IEnumerable<UserBarChart>> GetAllUserPerMonth()
    {
        DateTime currentMonth = DateTime.Now;
        var result = await _context.User.Where(a => a.CreationDate.Month <= currentMonth.Month).GroupBy(a => new { Month = a.CreationDate.Month })
            .OrderBy(g => g.Key.Month)
            .Select(g => new UserBarChart
            {
                Name = culture.DateTimeFormat.GetMonthName(g.Key.Month),
                Value = g.Count()
            })
        .ToListAsync();

        return result;
    }


}