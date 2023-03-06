using schedule_appointment_domain;
using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.Response;
using schedule_appointment_domain.Repositories;
using schedule_appointment_service.Interface;
using static schedule_appointment_domain.Model.ViewModels.ProfessionalViewModel;

namespace schedule_appointment_service.Services
{
    public class ProfessionalService : IProfessionalService
    {
        private readonly IProfessionalRepository _professionalRepository;
        private readonly IUnitOfWork _uow;

        public ProfessionalService(IProfessionalRepository professionalRepository, IUnitOfWork uow)
        {
            _professionalRepository = professionalRepository;
            _uow = uow;

        }
        public async Task<int> CreateAsync(ProfessionalCreateViewModel professionalCreateViewModel)
        {
            var professional = new Professional
            {
                Name = professionalCreateViewModel.Name
            };

            await _professionalRepository.CreateAsync(professional);
            try
            {
                await _uow.Commit();
            }
            catch (Exception e)
            {

            }
            return professional.Id;
        }

        public async Task<Page<ProfessionalListViewModel>> GetAllPageableAsync(ProfessionalFindListViewModel professionalPageableRequest)
        {
            var professionals = await _professionalRepository.GetAllPageableAsync(professionalPageableRequest);
            return professionals;
        }

        public async Task<ProfessionalFindViewModel?> GetByIdAsync(int id)
        {
            var client = await _professionalRepository.GetByIdAsync(id);

            if (client == null)
            {
                return new ProfessionalFindViewModel();
            }

            var obj = new ProfessionalFindViewModel
            {
                Id = client.Id,
                Name = client.Name 
            };

            return obj;
        }

        public async Task<IEnumerable<ProfessionalResponse>> GetProfessionals()
        {
            return await _professionalRepository.GetProfessionals();
        }

        public async Task Update(ProfessionalUpdateViewModel professionalUpdateViewModel)
        {
            var obj = new Professional
            {
                Name = professionalUpdateViewModel.Name,
                Id = professionalUpdateViewModel.Id
                
            };

            var professional = await _professionalRepository.GetByIdAsync(obj.Id);
            if (professional is null)
                throw new Exception();

            professional.Name = obj.Name; 

            try
            {
                _professionalRepository.Update(professional);
                await _uow.Commit();
            }
            catch (Exception e)
            {

            }
        }
    }
}
