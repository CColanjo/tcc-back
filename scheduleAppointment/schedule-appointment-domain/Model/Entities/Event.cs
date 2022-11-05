using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schedule_appointment_domain.Model.Entities
{
    public class Event
    {
        public int Id { get; set; } = default;
        public string PrimaryKey { get; set; } = string.Empty;
        public string Table { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string ColumnValues { get; set; } = string.Empty;
        public string? Changes { get; set; }
        public string Environment { get; set; } = string.Empty;
        public int User { get; set; } = default;
        public DateTime UpdatedDate { get; set; }
    }
}
