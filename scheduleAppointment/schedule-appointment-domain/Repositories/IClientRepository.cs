using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static schedule_appointment_domain.Model.ViewModels.ClientViewModel;

namespace schedule_appointment_domain.Repositories
{
    public interface IClientRepository : IRepositoryBase<Client>
    {
        Task<Client?> GetByIdAsync(int id);
        Task<Page<ClientListViewModel>> GetAllPageableAsync(ClientFindListViewModel clientPageableRequest);
        Task<IEnumerable<ClientResponse>> GetClients();
        Task<IEnumerable<ClientBarChart>> GetAllClientsPerMonth();
    }
}
