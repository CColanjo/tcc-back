using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schedule_appointment_service.Interface
{
    public interface ISendEmailService
    {

        public Task<string> SendEmailAsync(string email, string text, string name, string apiKey);
    }
}
