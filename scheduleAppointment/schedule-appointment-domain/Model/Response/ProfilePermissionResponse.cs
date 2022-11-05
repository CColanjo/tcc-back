namespace schedule_appointment_domain.Model.Response
{
    public class ProfilePermissionResponse
    {
        public int IdProfile { get; set; }
        public string NameProfile { get; set; } = string.Empty;
        public int EnabledProfile { get; set; }
        public int IdPermission { get; set; }
        public string NamePermission { get; set; } = string.Empty;
        public int EnabledPermission { get; set; }
    }
}
