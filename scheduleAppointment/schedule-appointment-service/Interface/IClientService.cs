

using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.Response;
using static schedule_appointment_domain.Model.ViewModels.ClientViewModel;

namespace schedule_appointment_service.Interface
{
    public interface IClientService
    {
        Task<int> CreateAsync(ClientCreateViewModel clientCreateViewModel);
        Task Update(ClientUpdateViewModel clientUpdateViewModel);
        Task<ClientFindViewModel?> GetByIdAsync(int id);

        Task<Page<ClientListViewModel>> GetAllPageableAsync(ClientFindListViewModel clientPageableRequest);
        Task<IEnumerable<ClientResponse>> GetClients();
    }
}
