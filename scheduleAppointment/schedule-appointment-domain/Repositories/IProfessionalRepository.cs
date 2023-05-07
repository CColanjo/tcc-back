using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.Response;
using static schedule_appointment_domain.Model.ViewModels.ProfessionalViewModel;


namespace schedule_appointment_domain.Repositories
{
    public interface IProfessionalRepository : IRepositoryBase<Professional>
    {
        Task<Professional?> GetByIdAsync(int id);
        Task<Page<ProfessionalListViewModel>> GetAllPageableAsync(ProfessionalFindListViewModel professionalPageableRequest);
        Task<IEnumerable<ProfessionalResponse>> GetProfessionals();
        Task<IEnumerable<ProfessionalBarChart>> GetAllProfessionalPerMonth();
    }
}
