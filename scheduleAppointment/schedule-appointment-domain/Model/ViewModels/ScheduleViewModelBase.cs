using schedule_appointment_domain.Model.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schedule_appointment_domain.Model.ViewModels
{
    public abstract record ScheduleViewModelBase
    {
        public int ClientId { get; set; }
        public DateTime ScheduleDate { get; set; }
        public bool WillAttend { get; set; }
        public int ProfessionalId { get; set; }
    }
    public record ScheduleCreateViewModel : ScheduleViewModelBase
    {

    }

    public record ScheduleUpdateViewModel : ScheduleViewModelBase
    {
        public int Id { get; init; } = default!;

    }
    public record ScheduleFindViewModel : ScheduleViewModelBase
    {
        public int Id { get; init; } = default!;
        public string ClientName { get; init; } = default!;

    }

    public record ScheduleListViewModel : ScheduleViewModelBase
    {
        public int Id { get; init; } = default!;
        public string NameClient { get; init; } = default!;

        public string NameProfessional{ get; init; } = default!;
    }

    public class ScheduleFindListViewModel : Pageable
    { 

    }

    public record ScheduleBarChart 
    {
        public string Name { get; init; } = default!;
        public int Value { get; init; } = default!;
    }
}
