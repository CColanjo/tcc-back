namespace schedule_appointment_domain.Model.Request
{
    public record OAuthRequest
    {
        /// <summary>
        /// Email
        /// </summary>        
        public string Username { get; init; } = default!;

        /// <summary>
        /// Password
        /// </summary>        
        public string Password { get; init; } = default!;
    }
}
