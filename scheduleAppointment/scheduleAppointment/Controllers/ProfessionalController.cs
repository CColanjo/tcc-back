using Microsoft.AspNetCore.Mvc;
using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.Response;
using schedule_appointment_domain.Model.ViewModels;
using schedule_appointment_service.Interface;
using schedule_appointment_service.Services;
using static schedule_appointment_domain.Model.ViewModels.ClientViewModel;
using static schedule_appointment_domain.Model.ViewModels.ProfessionalViewModel;

namespace scheduleAppointment.Controllers
{
    public class ProfessionalController : BaseApiController
    {
        private readonly IProfessionalService _service;
        public ProfessionalController(IProfessionalService service)
        {
            _service = service;
        }

        [HttpPost("professioanl")]
        public async Task CreateProfessional([FromBody] ProfessionalCreateViewModel professionalCreateViewModel)
        {
            await _service.CreateAsync(professionalCreateViewModel);
        }

        [HttpPut("professional")]
        public async Task UpdateClient([FromBody] ProfessionalUpdateViewModel professionalUpdateViewModel)
        {
            await _service.Update(professionalUpdateViewModel);
        }

        [HttpGet("professional")]
        public async Task<ProfessionalFindViewModel?> GetProfessional(int id)
        {
            return await _service.GetByIdAsync(id);
        }

        [HttpGet("professionals")]
        public async Task<IEnumerable<ProfessionalResponse>> GetProfessional()
        {
            return await _service.GetProfessionals();
        }

        [HttpGet("professionals/paginated")]
        public async Task<Page<ProfessionalListViewModel>> GetAllProfessionalPageable([FromQuery] ProfessionalFindListViewModel pageableRequest)
        {
            return await _service.GetAllPageableAsync(pageableRequest);
        } 
    }
}
