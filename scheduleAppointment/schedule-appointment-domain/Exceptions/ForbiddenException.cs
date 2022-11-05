using System.Net;

namespace schedule_appointment_domain.Exceptions
{
    public class ForbiddenException : Exception
    {
        public HttpStatusCode HttpStatus { get; set; }

        public ForbiddenException()
        {
            HttpStatus = System.Net.HttpStatusCode.Forbidden;
        }

        public ForbiddenException(string message)
            : base(message)
        {
            HttpStatus = HttpStatusCode.Forbidden;
        }

        public ForbiddenException(string message, HttpStatusCode httpStatus)
            : base(message)
        {
            HttpStatus = httpStatus;
        }
    }
}
