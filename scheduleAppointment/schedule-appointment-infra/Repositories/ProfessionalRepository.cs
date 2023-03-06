using AutoMapper;
using Microsoft.EntityFrameworkCore;
using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.Response;
using schedule_appointment_domain.Model.ViewModels;
using schedule_appointment_domain.Repositories;
using schedule_appointment_infra.Extensions;
using static schedule_appointment_domain.Model.ViewModels.ProfessionalViewModel;

namespace schedule_appointment_infra.Repositories
{
    public class ProfessionalRepository : RepositoryBase<Professional>, IProfessionalRepository
    {
        private readonly IMapper _mapper;
        public ProfessionalRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Page<ProfessionalListViewModel>> GetAllPageableAsync(ProfessionalFindListViewModel professionalPageableRequest)
        {
            return await _context.Professional.AsNoTracking().PageAsync<Professional, ProfessionalListViewModel>(professionalPageableRequest, _mapper);
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
