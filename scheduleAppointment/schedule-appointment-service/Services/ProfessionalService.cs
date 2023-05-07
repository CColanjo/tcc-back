using Microsoft.Extensions.Localization;
using schedule_appointment_domain;
using schedule_appointment_domain.Exceptions;
using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.Response;
using schedule_appointment_domain.Repositories;
using schedule_appointment_service.Interface;
using schedule_appointment_service.Localize;
using System.Collections.Generic;
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
                Name = professionalCreateViewModel.Name,
                CreationDate = DateTime.UtcNow,
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

        public async Task<IEnumerable<ProfessionalBarChart>> GetAllProfessionalPerMonth()
        {
            return await _professionalRepository.GetAllProfessionalPerMonth();
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

            var obj = new Professional
            {
                Name = professionalUpdateViewModel.Name,
                Id = professionalUpdateViewModel.Id

            };

            var professional = await _professionalRepository.GetByIdAsync(obj.Id);
            if (professional is null)
                throw new Exception();

            professional.Name = obj.Name;

            _professionalRepository.Update(professional);

            try
            {
                await _uow.Commit();

            }
            catch (Exception e)
            {

            }

            return obj.Id;
        }
    }
}
