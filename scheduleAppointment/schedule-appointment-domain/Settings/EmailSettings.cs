namespace schedule_appointment_domain.Settings
{
    public class EmailSettings
    {
        public string SmtpHost { get; set; } = default!;
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; } = default!;
        public string SmtpPassword { get; set; } = default!;
        public bool CheckCertificateRevocation { get; set; }
        public int MailKitSecurity { get; set; }
        public string SenderName { get; set; } = default!;
        public string SenderAddress { get; set; } = default!;
    }
}