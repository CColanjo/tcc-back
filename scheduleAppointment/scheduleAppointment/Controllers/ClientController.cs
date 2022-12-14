
using Microsoft.AspNetCore.Mvc;
using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.Response;
using schedule_appointment_service.Interface;
using scheduleAppointment.Controllers;
using static schedule_appointment_domain.Model.ViewModels.ClientViewModel;

namespace Default.Project.Api.Controllers
{
    public class ClientController : BaseApiController
    {
        private readonly IClientService _clientService;
     
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
     
        }

        [HttpPost("client")]
        public async Task CreateClient([FromBody] ClientCreateViewModel clientCreateViewModel)
        {
            await _clientService.CreateAsync(clientCreateViewModel);
        }


        [HttpPut("client")]
        public async Task UpdateClient([FromBody] ClientUpdateViewModel clientUpdateViewModel)
        {
            await _clientService.Update(clientUpdateViewModel);
        }

        [HttpGet("client")]
        public async Task<ClientFindViewModel?> GetClient(int id)
        {
            return await _clientService.GetByIdAsync(id);
        }


        [HttpGet("clients")]
        public async Task<IEnumerable<ClientResponse>> GetClients()
        {
            return await _clientService.GetClients();
        }

        [HttpGet("clients/paginated")]
        public async Task<Page<ClientListViewModel>> GetAllUsersPageable([FromQuery] ClientFindListViewModel pageableRequest)
        {
            return await _clientService.GetAllPageableAsync(pageableRequest);
        }
    }
}


