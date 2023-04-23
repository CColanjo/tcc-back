using schedule_appointment_service.Interface;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace schedule_appointment_service.Services
{
    public class SendEmail : ISendEmail
    {
        public async Task<string> SendEmailAsync(string email, string text, string name)
        {
            try
            {
                var apiKey = "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";
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("c.colanjo@gmail.com", "Camila");
                var subject = "Password";
                var to = new EmailAddress(email, name);
                var plainTextContent = "";
                var htmlContent = "<strong>"+text+"</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);

                return response.StatusCode.ToString();
            }
            catch (Exception e) {
                throw e;
            }
            
        }
    }
}
