namespace schedule_appointment_domain.Exceptions
{
    public class DataBaseException : Exception
    {
        public const string ExceptionType = "DataBaseException";
        public DataBaseException()
        {

        }

        public DataBaseException(Exception exception) : base(exception.GetType().Name, exception)
        {

        }

        public DataBaseException(string message) : base(message)
        {

        }

        public DataBaseException(string message, System.Exception innerException) : base(message, innerException)
        {

        }
    }
}
