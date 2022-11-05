namespace schedule_appointment_domain.Settings
{
    public class TokenSettings
    {
        public string Audience { get; set; } = default!;
        public string Issuer { get; set; } = default!;
        public int Seconds { get; set; }
    }
}