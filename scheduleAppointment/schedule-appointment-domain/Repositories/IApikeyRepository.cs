using schedule_appointment_domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schedule_appointment_domain.Repositories
{
    public interface IApikeyRepository
    {

        public Task<Apikey> GetApikey(string name);
    }
}
