using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using schedule_appointment_domain;
using schedule_appointment_domain.Helpers;
using schedule_appointment_domain.Model.Entities;
using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.Response;
using schedule_appointment_domain.Model.ViewModels;
using schedule_appointment_domain.Repositories;
using schedule_appointment_service.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                ScheduleDate = scheduleCreateViewModel.ScheduleDate.AddHours(3),
                WillAttend = true,
                ClientId = scheduleCreateViewModel.ClientId,
                CreationDate = DateTime.UtcNow,
                ProfessionalId = scheduleCreateViewModel.ProfessionalId
            };

            try {
                await _scheduleRepository.CreateAsync(schedule);
                await _uow.Commit();
            }
            catch (Exception e){
             
            } 
            return schedule.Id;
        }

        public async Task Disable(int id)
        {

            var schedule = await _scheduleRepository.GetByIdAsync(id);
            if (schedule is null)
                throw new Exception();

            schedule.WillAttend = false;

            try
            {
                _scheduleRepository.Update(schedule);
                await _uow.Commit();
            }
            catch (Exception e)
            {
               
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
                ScheduleDate = schedule.ScheduleDate,
                ProfessionalId = schedule.ProfessionalId,
            };

            return obj;
        }

        public async Task<Page<ScheduleListViewModel>> GetAllPageableAsync(ScheduleFindListViewModel clientPageableRequest)
        {
            var schedules = await _scheduleRepository.GetAllPageableAsync(clientPageableRequest);
            return schedules;
        }
        public async Task SendMessage()
        {   
            DateTime dateTime = DateTime.Now.AddDays(1);
            var clients = await _scheduleRepository.GetByDateAsync(dateTime);
            Console.WriteLine("Aqui1");
            //if (!clients.IsNullOrEmpty())
            //{
            //    foreach (var client in clients)
            //   {
            //       SendMessageTwillio(client.NameClient, client.CellPhone, client.ScheduleDate, client.NameProfessional);
            //    }
            //} 
        }

        public void SendMessageTwillio(string name, string cellphone, DateTime date, string professionalName)
        {
            try
            {
                string accountSid = "ACd62af68b695a818d51690bb768ae22ee";
                string authToken = "1881ce8e14e3b583a0e757bd596bff56";

                TwilioClient.Init(accountSid, authToken);
                var message = MessageResource.Create(
                          body: "Olá "+ name +" você tem um horário marcado " + date.Day +"/"+ date.Month+"/"+date.Year +  " às " + date.Hour + ":" + date.Minute + " com " + professionalName,
                          from: new Twilio.Types.PhoneNumber("whatsapp:+14155238886"),
                          to: new Twilio.Types.PhoneNumber("whatsapp:+55" + cellphone)
                 );
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
