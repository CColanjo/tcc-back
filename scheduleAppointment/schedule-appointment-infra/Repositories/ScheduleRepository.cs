﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.Response;
using schedule_appointment_domain.Model.ViewModels;
using schedule_appointment_domain.Repositories;
using schedule_appointment_infra.Extensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace schedule_appointment_infra.Repositories
{
    public class ScheduleRepository : RepositoryBase<Schedule>, IScheduleRepository
    {
        private readonly IMapper _mapper;
        public ScheduleRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Page<ScheduleListViewModel>> GetAllPageableAsync(ScheduleFindListViewModel schedulePageableRequest)
        {
               var query = _context.Schedule
                .Join(_context.Client,
                   schedule => schedule.ClientId,
                   client => client.Id,
                   (schedule, client) => new { schedule, client })
                .Join(
                    _context.Professional,
                    combinedEntry => combinedEntry.schedule.ProfessionalId,
                    professional => professional.Id,
                    (combinedEntry, professional) => new ScheduleResponse
                    {
                        ClientId = combinedEntry.schedule.ClientId,
                        ScheduleDate = combinedEntry.schedule.ScheduleDate,
                        WillAttend = combinedEntry.schedule.WillAttend,
                        NameClient = combinedEntry.client.Name,
                        Id = combinedEntry.schedule.Id,
                        NameProfessional = professional.Name,
                        ProfessionalId = professional.Id
                    }
                )
                .Where(o => o.WillAttend == true).OrderBy(o => o.ScheduleDate);

            return await query.PageAsync<ScheduleResponse, ScheduleListViewModel>(schedulePageableRequest, _mapper);
        }

        public  async Task<IEnumerable<ScheduleResponse>> GetAllSchedules()
        {
           return await
           (
                from schedule in _context.Schedule.AsNoTracking()
                join client in _context.Client.AsNoTracking()
                on schedule.ClientId equals client.Id
                join professional in _context.Professional.AsNoTracking()
                on schedule.ProfessionalId equals professional.Id
                where
                    schedule.WillAttend == true
                orderby schedule.ScheduleDate
                select
                    new ScheduleResponse
                    {
                        ClientId = schedule.ClientId,
                        ScheduleDate = schedule.ScheduleDate,
                        WillAttend = schedule.WillAttend,
                        NameClient = client.Name,
                        Id = schedule.Id,
                        NameProfessional = professional.Name
                    }
            ).ToListAsync();
        }

        public async Task<IEnumerable<ScheduleResponse>> GetByDateAsync(DateTime scheduleDate)
        {
            return await
            (
                 from schedule in _context.Schedule.AsNoTracking()
                 join client in _context.Client.AsNoTracking()
                 on schedule.ClientId equals client.Id
                 join professional in _context.Professional.AsNoTracking()
                 on schedule.ProfessionalId equals professional.Id
                 where
                     schedule.ScheduleDate.Year == scheduleDate.Year &&
                     schedule.ScheduleDate.Month == scheduleDate.Month &&
                     schedule.ScheduleDate.Day == scheduleDate.Day &&
                     schedule.WillAttend == true
                 orderby schedule.ScheduleDate
                 select
                     new ScheduleResponse
                     {
                         ClientId = schedule.ClientId,
                         ScheduleDate = schedule.ScheduleDate,
                         WillAttend = schedule.WillAttend,
                         NameClient = client.Name,
                         Id = schedule.Id,
                         CellPhone = client.Cellphone,
                         NameProfessional =  professional.Name
                     }
             ).ToListAsync();
        }

        public async Task<Schedule?> GetByIdAsync(int id)
        { 
             return await _context.Schedule.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id); 
        }
    }
}
