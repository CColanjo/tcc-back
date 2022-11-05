using System.Globalization;

namespace schedule_appointment_domain.Constants
{
    public static class GlobalizationConstats
    {
        public static readonly string[] supportedCultures = { "en-US", "es-CO", "pt-BR" };

        public static readonly CultureInfo[] supportedCulturesTyped = { new CultureInfo("en-US"), new CultureInfo("es-CO"), new CultureInfo("pt-BR") };
    }
}
