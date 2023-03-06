using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.Response;
using schedule_appointment_domain.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schedule_appointment_domain.Repositories
{
    public interface IScheduleRepository :IRepositoryBase<Schedule>
    {
        Task<Schedule?> GetByIdAsync(int id);
        Task<Page<ScheduleListViewModel>> GetAllPageableAsync(ScheduleFindListViewModel schedulePageableRequest);
        Task<IEnumerable<ScheduleResponse>> GetByDateAsync(DateTime scheduleDate);
        Task<IEnumerable<ScheduleResponse>> GetAllSchedules(); 
    
    }
}
