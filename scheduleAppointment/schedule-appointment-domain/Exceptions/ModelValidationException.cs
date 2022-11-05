 
 
using Microsoft.AspNetCore.Mvc.ModelBinding;
using schedule_appointment_domain.Model;
using schedule_appointment_domain.Model.Response;

namespace schedule_appointment_domain.Exceptions
{
    public class ModelValidationException : Exception
    {
        private ModelStateDictionary ModelState { get; set; }

        private string BusinessCodeException { get; set; }

        public ModelValidationException()
        { }

        public ModelValidationException(string message)
            : base(message)
        { }

        public ModelValidationException(string message, string businessCodeException)
           : base(message)
        {
            BusinessCodeException = businessCodeException;
        }

        public ModelValidationException(string message, ModelStateDictionary modelState, string? businessCodeException = null)
            : base(message)
        {
            ModelState = modelState;
            BusinessCodeException = businessCodeException ?? string.Empty;
        }

       
    }
}
