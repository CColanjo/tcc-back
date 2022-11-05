using schedule_appointment_domain;
using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.Response;
using schedule_appointment_domain.Model.ViewModels;
using schedule_appointment_domain.Repositories;
using schedule_appointment_service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static schedule_appointment_domain.Model.ViewModels.ClientViewModel;

namespace schedule_appointment_service.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _uow;

        public ClientService(IClientRepository clientRepository, IUnitOfWork uow)
        {
            _clientRepository = clientRepository;
            _uow = uow;

        }

        public async Task<int> CreateAsync(ClientViewModel.ClientCreateViewModel clientCreateViewModel)
        {
            var client = new Client
            {
                Name = clientCreateViewModel.Name,
                Cellphone = clientCreateViewModel.Cellphone,
                Email = clientCreateViewModel.Email,
                Address = clientCreateViewModel.Address,
                CreationDate = DateTime.UtcNow,

            };

            await _clientRepository.CreateAsync(client);
            try 
            {
                await _uow.Commit();
            }
            catch (Exception e){
               
            } 
            return client.Id;
        } 

        public async Task<ClientFindViewModel?> GetByIdAsync(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);

            if (client == null)
            {
                return new ClientFindViewModel();
            }

            var obj = new ClientFindViewModel
            {
                Id = client.Id,
                Name = client.Name,
                Cellphone = client.Cellphone,
                Email = client.Email,
                Address = client.Address,
                 
            };

            return obj;
        }

        public async Task<IEnumerable<ClientResponse>> GetClients()
        {
            return await _clientRepository.GetClients();
        }

        public async Task Update(ClientViewModel.ClientUpdateViewModel clientUpdateViewModel)
        {
            var obj = new Client
            {
                Name = clientUpdateViewModel.Name,
                Cellphone = clientUpdateViewModel.Cellphone,
                Email = clientUpdateViewModel.Email,
                Address = clientUpdateViewModel.Address,
                Id = clientUpdateViewModel.Id,
            };

            try
            {

                var client = await _clientRepository.GetByIdAsync(obj.Id);
                if (client is null)
                    throw new Exception();

                client.Name = obj.Name;
                client.Cellphone = obj.Cellphone;
                client.Email = obj.Email;
                client.Address = obj.Address;

                _clientRepository.Update(client);
                await _uow.Commit();
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }

         public  async Task<Page<ClientListViewModel>> GetAllPageableAsync(ClientFindListViewModel clientPageableRequest)
        {
            var clients = await _clientRepository.GetAllPageableAsync(clientPageableRequest);
            return clients;

        }
    }
}
