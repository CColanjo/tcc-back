using Newtonsoft.Json;

namespace schedule_appointment_domain.Model.Response
{
    public class TokenResponse
    {
        /// <summary>
        /// Username
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; } = default!;

        /// <summary>
        /// Email
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; } = default!;

        /// <summary>
        /// Token Value
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; } = default!;

        /// <summary>
        /// Refresh Token Value
        /// </summary>
        [JsonProperty("refreshtoken")]
        public string RefreshToken { get; set; } = default!;

        /// <summary>
        /// Token Created Date
        /// </summary>
        [JsonProperty("created")]
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// Token ExpirationDate
        /// </summary>
        [JsonProperty("expires")]
        public DateTimeOffset Expires { get; set; }
 
        public Boolean isAdmin { get; set; }
    }
}
