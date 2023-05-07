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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static schedule_appointment_domain.Model.ViewModels.ClientViewModel;

namespace schedule_appointment_infra.Repositories
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        private CultureInfo culture = new CultureInfo("pt-BR");
        private readonly IMapper _mapper;
        public ClientRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClientBarChart>> GetAllClientsPerMonth()
        {
            DateTime currentMonth = DateTime.Now;
            var result = await _context.Client.Where(a =>  a.CreationDate.Month <= currentMonth.Month).GroupBy(a => new { Month = a.CreationDate.Month })
                .OrderBy(g => g.Key.Month)
                .Select(g => new ClientBarChart
                {
                    Name = culture.DateTimeFormat.GetMonthName(g.Key.Month),
                    Value = g.Count()
                })
            .ToListAsync();

            return result;
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
