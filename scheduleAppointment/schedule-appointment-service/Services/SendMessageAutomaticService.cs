using schedule_appointment_domain;
using schedule_appointment_domain.Model.Response;
using schedule_appointment_domain.Repositories;
using schedule_appointment_service.Interface;
using System.Collections.Generic;
using Twilio;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace schedule_appointment_service.Services
{
    public class SendMessageAutomaticService : ISendMessageAutomaticService
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IUnitOfWork _uow;
        public SendMessageAutomaticService(IScheduleRepository scheduleRepository, IUnitOfWork uow)
        {
            _scheduleRepository = scheduleRepository;
            _uow = uow;
        }

       

        public async Task getClientsToSendMessageAsync ()
        { 

            try
            {
                var teste = await _scheduleRepository.GetByDateNowAsync();
                teste.ToList().ForEach(i=> sendMessage(i.Name, i.Cellphone));
            

                //foreach (SendMessageAutomaticResponse client in listClients) {
                //    sendMessage(client.Name, client.Cellphone); 
                //}

            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }

        public void sendMessage(string name, string cellphone)
        {
         
            //try
            //{
            //    string accountSid = "ACd62af68b695a818d51690bb768ae22ee";
            //    string authToken = "dc958724efbe2e8aade810449a953fab";

            //    TwilioClient.Init(accountSid, authToken);

            //    var messageOptions = new CreateMessageOptions(
            //        new PhoneNumber("whatsapp:"+ cellphone));
            //    messageOptions.From = new PhoneNumber("whatsapp:+14155238886");
            //    messageOptions.Body = "Oi " + name ;

            //    var message = MessageResource.Create(messageOptions);
            //    Console.WriteLine(message.Body);

            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.ToString());
            //}
        }
    }
}
