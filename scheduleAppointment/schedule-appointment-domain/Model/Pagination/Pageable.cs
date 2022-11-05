using schedule_appointment_domain.Model.Pagination.Interfaces;

namespace schedule_appointment_domain.Model.Pagination
{
    public class Pageable : IPageable
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
