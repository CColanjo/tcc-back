using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.Response;
using schedule_appointment_domain.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schedule_appointment_service.Interface
{
    public interface IScheduleService
    {
        Task<int> CreateAsync(ScheduleCreateViewModel scheduleCreateViewModel);
        Task Disable(int id);
        Task<ScheduleFindViewModel?> GetByIdAsync(int id); 
        Task<Page<ScheduleListViewModel>> GetAllPageableAsync(ScheduleFindListViewModel schedulePageableRequest);
        Task<IEnumerable<ScheduleResponse>> GetByDateAsync(DateTime date);
        Task<IEnumerable<ScheduleResponse>> GetAllSchedules();
    }
}
