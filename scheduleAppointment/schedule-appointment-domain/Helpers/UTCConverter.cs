using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schedule_appointment_domain.Helpers
{
    public static class UTCConverter
    {
        public static DateTime ConvertFromUtc(this DateTime utcValue, string culture)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(utcValue, TimeZoneInfo.FindSystemTimeZoneById(_timeZones[culture.ToLower()]));
        }

        private static readonly Dictionary<string, string> _timeZones = new()
    {
        { "pt-br", "E. South America Standard Time" },
        { "es-co", "SA Pacific Standard Time" },
    };
    }
}
