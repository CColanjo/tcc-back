using Microsoft.Extensions.Localization;
using schedule_appointment_domain;
using schedule_appointment_domain.Exceptions;
using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.Response;
using schedule_appointment_domain.Repositories;
using schedule_appointment_service.Interface;
using schedule_appointment_service.Localize;
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
            try
            {
                var professional = new Professional {
                    Name = professionalCreateViewModel.Name
                };

                await _professionalRepository.CreateAsync(professional);
                await _uow.Commit();
                return professional.Id;
            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um erro, aguarde ou entre em contato com o responsável");
            }
           
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

        public async Task<int> Update(ProfessionalUpdateViewModel professionalUpdateViewModel)
        {
            try
            {
                var obj = new Professional {
                    Name = professionalUpdateViewModel.Name,
                    Id = professionalUpdateViewModel.Id

                };

                var professional = await _professionalRepository.GetByIdAsync(obj.Id);
                if (professional is null)
                    throw new Exception();

                professional.Name = obj.Name;

                _professionalRepository.Update(professional);
                await _uow.Commit();
                return obj.Id;
            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um erro, aguarde ou entre em contato com o responsável");
            }  
        }
    }
}
