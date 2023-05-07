using Microsoft.AspNetCore.Mvc;
using schedule_appointment.Utils;
using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.ViewModels;
using schedule_appointment_service.Interface;
using schedule_appointment_service.Services;
using scheduleAppointment.Controllers;
using static schedule_appointment_domain.Model.ViewModels.ClientViewModel;

namespace Default.Project.Api.Controllers
{ 
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;
       
        public UserController(IUserService userService)
        {
            _userService = userService;
          
        }

        [ClaimAuthorize("Admin")]
        [HttpPost("user")]
        public async Task<int> CreateUser([FromBody] UserCreateViewModel userCreateViewModel)
        {
          return  await _userService.CreateAsync(userCreateViewModel);
        }

        
        [HttpPut("user")]
        public async Task UpdateUser([FromBody] UserUpdateViewModel userUpdateViewModel)
        {
            await _userService.Update(userUpdateViewModel);
        }

       
        [HttpGet("user")]
        public async Task<UserFindViewModel?> GetUserById(int id)
        {
           return await _userService.GetByIdAsync(id);
        }
        
        [HttpPut("user/disable")]
        public async Task UpdateDisable(int id)
        {
            await _userService.Disable(id);
        }


        [HttpPut("user/active")]
        public async Task UpdateActive(int id)
        {
            await _userService.Active(id);
        }

        [HttpGet("users/paginated")]
        public async Task<Page<UserListViewModel>> GetAllUsersPageable([FromQuery] UserFindListViewModel pageableRequest)
        {
            return await _userService.GetAllPageableAsync(pageableRequest);
        }

        [HttpGet("users/GetAllUserPerMonth")]
        public async Task<IEnumerable<UserBarChart>> GetAllUserPerMonth()
        {
            return await _userService.GetAllUserPerMonth();
        }
    }

}
