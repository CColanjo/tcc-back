using AutoMapper;
using Microsoft.EntityFrameworkCore;
using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.Response;
using schedule_appointment_domain.Model.ViewModels;
using schedule_appointment_domain.Repositories;
using schedule_appointment_infra.Extensions;
using static schedule_appointment_domain.Model.ViewModels.ClientViewModel;
using System.Text.RegularExpressions;
using static schedule_appointment_domain.Model.ViewModels.ProfessionalViewModel;
using System.Globalization;

namespace schedule_appointment_infra.Repositories
{
    public class ProfessionalRepository : RepositoryBase<Professional>, IProfessionalRepository
    {
        private readonly IMapper _mapper;
        private CultureInfo culture = new CultureInfo("pt-BR");
        public ProfessionalRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Page<ProfessionalListViewModel>> GetAllPageableAsync(ProfessionalFindListViewModel professionalPageableRequest)
        {
            return await _context.Professional.AsNoTracking().PageAsync<Professional, ProfessionalListViewModel>(professionalPageableRequest, _mapper);
        }

        public async Task<IEnumerable<ProfessionalBarChart>> GetAllProfessionalPerMonth()
        {
            DateTime currentMonth = DateTime.Now;
            var result = await _context.Professional.Where(a => a.CreationDate.Month <= currentMonth.Month).GroupBy(a => new { Month = a.CreationDate.Month })
            .OrderBy(g => g.Key.Month)
                .Select(g => new ProfessionalBarChart
                {
                    Name = culture.DateTimeFormat.GetMonthName(g.Key.Month),
                    Value = g.Count()
                })
            .ToListAsync();

            return result;
        }
         

        public async Task<Professional?> GetByIdAsync(int id)
        {
            return await _context.Professional.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<ProfessionalResponse>> GetProfessionals()
        {
            return await
           (
                from professioanl in _context.Professional.AsNoTracking()

                select
                    new ProfessionalResponse
                    {
                        Id = professioanl.Id,
                        Name = professioanl.Name
                    }
            ).ToListAsync();
        }
    }
}
