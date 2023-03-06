using schedule_appointment_domain.Model.Pagination;


namespace schedule_appointment_domain.Model.ViewModels
{
    public class ProfessionalViewModel
    {
        public abstract record ProfessionalViewModelBase
        {
            public string Name { get; init; } = default!; 
        }

        public record ProfessionalCreateViewModel : ProfessionalViewModelBase
        {

        }

        public record ProfessionalUpdateViewModel : ProfessionalViewModelBase
        {
            public int Id { get; init; } = default!;

        }
        public record ProfessionalFindViewModel : ProfessionalViewModelBase
        {
            public int Id { get; init; } = default!; 
        }

        public class ProfessionalFindListViewModel : Pageable
        {
            public string? Name { get; set; }
        }

        public record ProfessionalListViewModel : ProfessionalViewModelBase
        {
            public int Id { get; init; } = default!;

        }

       
    }
}
