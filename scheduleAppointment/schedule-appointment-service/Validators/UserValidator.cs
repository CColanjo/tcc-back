using FluentValidation;
using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Repositories;

using Microsoft.Extensions.Localization;
using schedule_appointment_service.Localize;

namespace schedule_appointment_service.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator(IUserRepository userRepository, IStringLocalizer<Resource> localizer)
    {
      
    }
}