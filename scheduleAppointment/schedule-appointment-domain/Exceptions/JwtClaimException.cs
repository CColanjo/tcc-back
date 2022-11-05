namespace schedule_appointment_domain.Exceptions
{
    public class JwtClaimException : Exception
    {
        public JwtClaimException()
        { }

        public JwtClaimException(string message)
            : base(message)
        {
        }
    }
}
