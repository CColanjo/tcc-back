using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schedule_appointment_domain.Model.Entities
{
    public class Schedule : Entity
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime ScheduleDate { get; set; }
        public bool WillAttend { get; set; }
         
    }
}
