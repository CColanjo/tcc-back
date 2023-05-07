using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.Response;
using schedule_appointment_domain.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static schedule_appointment_domain.Model.ViewModels.ClientViewModel;
using static schedule_appointment_domain.Model.ViewModels.ProfessionalViewModel;

namespace schedule_appointment_service.Interface
{
    public interface IProfessionalService
    {
        Task<int> CreateAsync(ProfessionalCreateViewModel professionalCreateViewModel);
        Task<int> Update(ProfessionalUpdateViewModel professionalUpdateViewModel);
        Task<ProfessionalFindViewModel?> GetByIdAsync(int id);
        Task<Page<ProfessionalListViewModel>> GetAllPageableAsync(ProfessionalFindListViewModel professionalPageableRequest);
        Task<IEnumerable<ProfessionalResponse>> GetProfessionals();
        Task<IEnumerable<ProfessionalBarChart>> GetAllProfessionalPerMonth();
        
    }
}
