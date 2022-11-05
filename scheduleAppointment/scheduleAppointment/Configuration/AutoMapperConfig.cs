
using AutoMapper;
using Microsoft.Extensions.Localization;
using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Model.Response;
using schedule_appointment_domain.Model.ViewModels;
using schedule_appointment_service.Localize;
using static schedule_appointment_domain.Model.ViewModels.ClientViewModel;

namespace scheduleAppointment.Configuration;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig(IStringLocalizer<Resource> localizer)
    {  
        CreateMap<User, UserListViewModel>();
        CreateMap<Client, ClientListViewModel>();
        CreateMap<Schedule, ScheduleListViewModel>();
        CreateMap<ScheduleResponse, ScheduleListViewModel>();
    }
}