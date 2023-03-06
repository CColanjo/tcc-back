using AutoMapper;
using Microsoft.EntityFrameworkCore;
using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.Response;
using schedule_appointment_domain.Model.ViewModels;
using schedule_appointment_domain.Repositories;
using schedule_appointment_infra.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static schedule_appointment_domain.Model.ViewModels.ClientViewModel;

namespace schedule_appointment_infra.Repositories
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        private readonly IMapper _mapper;
        public ClientRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Page<ClientListViewModel>> GetAllPageableAsync(ClientFindListViewModel userPageableRequest)
        {
            return await _context.Client.AsNoTracking().PageAsync<Client, ClientListViewModel>(userPageableRequest, _mapper);
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            return await _context.Client.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<ClientResponse>> GetClients()
        {
            return await
            (
                 from client in _context.Client.AsNoTracking()

                 select
                     new ClientResponse
                     {
                         Id = client.Id,
                         Name = client.Name
                     }
             ).ToListAsync();
        }
    }
}
