namespace schedule_appointment_domain.Model.Pagination.Interfaces
{
    public interface IPageable
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
    }
}
