using AutoMapper;
using Microsoft.EntityFrameworkCore;
using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Repositories;

namespace schedule_appointment_infra.Repositories
{
    public class ApikeyRepository :  RepositoryBase<Apikey>, IApikeyRepository
    {
        private readonly IMapper _mapper;
        public ApikeyRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Apikey> GetApikey(string name)
        {
            return await _context.Apikey.AsNoTracking().FirstOrDefaultAsync(u => u.Name == name);
        }
    }
}
