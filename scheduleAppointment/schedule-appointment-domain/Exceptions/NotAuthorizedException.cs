using System.Net;

namespace schedule_appointment_domain.Exceptions
{
    [Serializable]
    public class NotAuthorizedException : Exception
    {
        public HttpStatusCode HttpStatus { get; set; }

        public NotAuthorizedException(string message, string Code)
            : base(message)
        {
            this.HttpStatus = HttpStatusCode.Unauthorized;
        }

        public NotAuthorizedException(string message, HttpStatusCode httpStatus)
            : base(message)
        {
            this.HttpStatus = httpStatus;
        }
    }
}
