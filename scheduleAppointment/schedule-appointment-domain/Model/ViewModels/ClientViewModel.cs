using schedule_appointment_domain.Model.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schedule_appointment_domain.Model.ViewModels
{
    public class ClientViewModel
    {

        public abstract record ClientViewModelBase
        {
            public string Name { get; init; } = default!;
            public string Cellphone { get; init; } = default!;
            public string Email { get; init; } = default!;
            public string Address { get; init; } = default!;

        }

        public record ClientCreateViewModel : ClientViewModelBase
        {

        }

        public record ClientUpdateViewModel : ClientViewModelBase
        {
            public int Id { get; init; } = default!;

        }
        public record ClientFindViewModel : ClientViewModelBase
        {
            public int Id { get; init; } = default!;
           

        }

        public class ClientFindListViewModel : Pageable
        {
            public string? Name { get; set; }
        }

        public record ClientListViewModel : ClientViewModelBase
        {
            public int Id { get; init; } = default!; 
             
        }
    }
}
