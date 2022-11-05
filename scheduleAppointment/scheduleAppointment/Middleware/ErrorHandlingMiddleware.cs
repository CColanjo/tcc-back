using schedule_appointment_domain.Exceptions;
using schedule_appointment_domain.Model;
using Newtonsoft.Json;
using System.Net;
using FluentValidation;
using schedule_appointment_domain.Model.Response;


namespace schedule_appointment.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;

            if (ex is NotAuthorizedException notAuthorizedException)
            {
                if (notAuthorizedException.HttpStatus == HttpStatusCode.NotFound)
                    code = HttpStatusCode.NotFound;
                else
                    code = HttpStatusCode.Unauthorized;
            }
            else if (ex is JwtClaimException)
                code = HttpStatusCode.Unauthorized;
            else if (ex is ModelValidationException)
                code = HttpStatusCode.BadRequest;
            else if (ex is ArgumentException or ArgumentNullException)
                code = HttpStatusCode.BadRequest;
            else if (ex is NotFoundException)
                code = HttpStatusCode.NotFound;
            else if (ex is ValidationException)
                code = HttpStatusCode.BadRequest;

            var result = JsonConvert.SerializeObject(new ModelValidationResponse
            {
                Message = ex.Message,
                ErrorCode = (int)code,
                ModelErrors = ex is ValidationException ? (ex as ValidationException).Errors.Select(x => new ErrorResponse
                {
                    Property = x.PropertyName, 
                    Error = x.ErrorMessage
                }) : null
            });
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}