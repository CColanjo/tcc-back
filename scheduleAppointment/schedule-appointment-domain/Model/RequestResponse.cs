using Newtonsoft.Json;
using schedule_appointment_domain.Model.Response;

namespace schedule_appointment_domain.Model
{
    /// <summary>
    ///  Base response from all api requests
    /// </summary>
    public class RequestResponse
    {
        /// <summary>
        /// Informative message from request processing
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; } = default!;
    }

    public class ValidationBadRequestResponse : RequestResponse
    {
        /// <summary>
        /// Model validation error list
        /// </summary>
        [JsonProperty("errors")]
        public List<ErrorResponse> Errors { get; set; } = default!;

    }

    /// <summary>
    /// Base response from all api requests
    /// </summary>
    public class RequestResponse<TEntity> : RequestResponse where TEntity : class
    {
        /// <summary>
        /// Data result from request processing
        /// </summary>
        [JsonProperty("response")]
        public TEntity Response { get; set; } = default!;
    }

    /// <summary>
    /// Base response from all api requests
    /// </summary>
    public class ModelValidationResponse : RequestResponse
    {
        /// <summary>
        /// Data result from request processing
        /// </summary>
        [JsonProperty("errors")]
        public IEnumerable<ErrorResponse> ModelErrors { get; set; } = default!;

        /// <summary>
        /// Custom errorcode
        /// </summary>
        [JsonProperty("errorCode")]
        public int ErrorCode { get; set; } = default!;
    }
}
