using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using schedule_appointment_service.Interface;

namespace scheduleAppointment.Controllers
{

    public class SendMessageAutomaticController : BaseApiController
    {

        private readonly ISendMessageAutomaticService _service;
        public SendMessageAutomaticController(ISendMessageAutomaticService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        [HttpGet("sendMessage")]
        public async Task  SendMessage() 
        {
             _service.getClientsToSendMessageAsync(); 

        }
    }
}
