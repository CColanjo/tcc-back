namespace schedule_appointment_domain.Model.Response
{
    public class SendMessageAutomaticResponse
    {
        public string Name { get; set; }
        public string Cellphone { get; set; }
        public DateTime Date { get; set; }
        public bool WillAttend { get; set; }
    }
}
