namespace schedule_appointment_domain.Model.Entities
{
    public class User : Entity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public bool IsAdmin { get; set; }
        public string Email { get; set; }

       
    }
}
