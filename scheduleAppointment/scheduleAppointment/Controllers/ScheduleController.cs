
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X500;
using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.Response;
using schedule_appointment_domain.Model.ViewModels;
using schedule_appointment_service.Interface;
using scheduleAppointment.Controllers;

namespace Default.Project.Api.Controllers
{ 
    public class ScheduleController : BaseApiController
    {
        private readonly IScheduleService _service;
        public ScheduleController(IScheduleService service )
        {
            _service = service; 
        }


        [HttpPost("schedule")]
        public async Task CreateSchedule([FromBody] ScheduleCreateViewModel scheduleCreateViewModel)
        {
            await _service.CreateAsync(scheduleCreateViewModel);
        }


        [HttpPut("schedule/disable")]
        public async Task UpdateDisable(int id)
        {
           await _service.Disable(id);
        }


        [HttpGet("schedules/data")]
        public async Task<IEnumerable<ScheduleResponse>> GetByDateAsync(DateTime scheduleDate) {
            return await _service.GetByDateAsync(scheduleDate);
        }

        [HttpGet("schedule")]
        public async Task<ScheduleFindViewModel?> GetScheduleById(int id)
        { 
            return await _service.GetByIdAsync(id);
        }

        [HttpGet("schedules")]
        public async Task<IEnumerable<ScheduleResponse>> GetAllSchedules()
        {
            return await _service.GetAllSchedules();
        }

        [HttpGet("schedules/paginated")]
        public async Task<Page<ScheduleListViewModel>> GetAllUsersPageable([FromQuery] ScheduleFindListViewModel pageableRequest)
        {
            return await _service.GetAllPageableAsync(pageableRequest);
        }

        [HttpPost("schedule/sendMessage")]
        public async Task SendMessage()
        {
            RecurringJob.AddOrUpdate(() => _service.SendMessage(), Cron.Minutely());
            //RecurringJob.AddOrUpdate(() =>  _service.SendMessage(), Cron.Daily(9, 0));
        }
    }
}
