using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using schedule_appointment_domain.Enumerator;

namespace scheduleAppointment.Controllers
{
    [Route("{culture:culture}")]
    [ApiController]
    [Authorize]
    public class BaseApiController : ControllerBase
    {
        protected Guid IdUser
        {
            get
            {
                return User.Identity is not null && User.Identity.IsAuthenticated
                    ? new Guid(User.Claims.FirstOrDefault(x => x.Type == nameof(Claims.userId))!.Value)
                    : throw new UnauthorizedAccessException();
            }
        }
    }
 }
