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
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace schedule_appointment_service.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IUnitOfWork _uow;
        public ScheduleService(IScheduleRepository scheduleRepository, IUnitOfWork uow)
        {
            _scheduleRepository = scheduleRepository;
            _uow = uow;
        }

        public async Task<int> CreateAsync(ScheduleCreateViewModel scheduleCreateViewModel)
        {
            var schedule = new Schedule
            {
                ScheduleDate = scheduleCreateViewModel.ScheduleDate,
                WillAttend = true,
                ClientId = scheduleCreateViewModel.ClientId,
                CreationDate = DateTime.UtcNow,

            };

            await _scheduleRepository.CreateAsync(schedule);
            await _uow.Commit();
            return schedule.Id;
        }

        public async Task Disable(int id)
        {
            try
            {
                var schedule = await _scheduleRepository.GetByIdAsync(id);
                if (schedule is null)
                    throw new Exception();

                schedule.WillAttend = false;

                _scheduleRepository.Update(schedule);
                await _uow.Commit();
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }

        public Task<IEnumerable<ScheduleResponse>> GetByDateAsync(DateTime date)
        {
            return _scheduleRepository.GetByDateAsync(date);
        }

        public async Task<IEnumerable<ScheduleResponse>> GetAllSchedules()
        {
            return await _scheduleRepository.GetAllSchedules();
        }

        public async Task<ScheduleFindViewModel?> GetByIdAsync(int id)
        {
            var schedule = await _scheduleRepository.GetByIdAsync(id);

            var obj = new ScheduleFindViewModel
            {
                Id = schedule.Id,
                ClientId = schedule.ClientId,
                WillAttend = schedule.WillAttend,
                ScheduleDate = schedule.ScheduleDate
            };

            return obj;
        }

        public async Task<Page<ScheduleListViewModel>> GetAllPageableAsync(ScheduleFindListViewModel clientPageableRequest)
        {
            var schedules = await _scheduleRepository.GetAllPageableAsync(clientPageableRequest);
            return schedules;
        }

        public async Task GetClientsToSendMessageAsync(DateTime date)
        {
            try
            {
         
                var clients = await GetByDateAsync(date);
              //  clients.ToList().ForEach(i=> sendMessage(i.NameClient, i.CellPhone, i.ScheduleDate)); 
            }
            catch (Exception e)
            {

                throw new Exception();
            }
        }

        public void sendMessage(string name, string cellphone, string date)
        {
            try
            {
                string accountSid = "ACd62af68b695a818d51690bb768ae22ee";
                string authToken = "dc958724efbe2e8aade810449a953fab";

                TwilioClient.Init(accountSid, authToken);

                var messageOptions = new CreateMessageOptions(
                new PhoneNumber("whatsapp:" + cellphone));
                messageOptions.From = new PhoneNumber("whatsapp:+14155238886");
                messageOptions.Body = "Oi " + name + " você tem uma horário marcada para" + date;

                var message = MessageResource.Create(messageOptions);
                Console.WriteLine(message.Body);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
