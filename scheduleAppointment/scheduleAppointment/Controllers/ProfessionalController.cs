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

        [HttpGet("professionals/GetAllProfessionalPerMonth")]
        public async Task<IEnumerable<ProfessionalBarChart>> GetAllProfessionalPerMonth()
        {
            return await _service.GetAllProfessionalPerMonth();
        }

        [HttpGet("professionals/excel")]
        public async Task<IActionResult> GenerateExcel()
        {
            byte[] excelBytes = await _service.GenerateExcel();

            // Set the file name for the downloaded file
            string fileName = "myfile.xlsx";

            // Set the MIME type for the Excel file
            string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            // Return the Excel file as the HTTP response
            return File(excelBytes, mimeType, fileName);
        }
    }
}
