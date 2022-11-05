using FluentValidation;

namespace schedule_appointment_service.Services;

public abstract class ServiceBase<T>
{
    private readonly IValidator<T> _validator;
    
    protected ServiceBase(IValidator<T> validator)
    {
        _validator = validator;
    }

    protected async Task ValidateAsync(T entity)
    {
        var validationResult = await _validator.ValidateAsync(entity);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
    }
}