using schedule_appointment_domain.Model.Pagination;

namespace schedule_appointment_domain.Model.ViewModels
{
    public abstract record UserViewModelBase
    {
        public string Name { get; init; } = default!;
        public string Username { get; init; } = default!;

    }

    public record UserCreateViewModel : UserViewModelBase
    {
        public bool IsAdmin { get; init; }
    }

    public record UserUpdateViewModel : UserViewModelBase
    {
        public int Id { get; init; } = default!;
        public bool Active { get; init; }
        public bool IsAdmin { get; init; }
    }
    public record UserFindViewModel : UserViewModelBase
    {
        public int Id { get; init; } = default!;
        public bool Active { get; init; }
        public bool IsAdmin { get; init; }
    }

    public record UserListViewModel : UserViewModelBase
    {
        public int Id { get; init; } = default!;
        public bool Active { get; init; }

    }

    public class UserFindListViewModel : Pageable
    {
        public string? Name { get; set; }
        public bool Active { get; init; }

    }
}
