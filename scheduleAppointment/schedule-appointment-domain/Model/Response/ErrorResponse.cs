using Newtonsoft.Json;

namespace schedule_appointment_domain.Model.Response
{
    public class ErrorResponse
    {
        [JsonProperty("property")]
        public string Property { get; set; } = default!;

        [JsonProperty("error")]
        public string Error { get; set; } = default!;
    }
}
