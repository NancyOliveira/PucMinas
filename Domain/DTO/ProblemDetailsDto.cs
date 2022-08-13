using Newtonsoft.Json;
using System.Net;

namespace Domain.DTO
{
    public class ProblemDetailsDto
    {
        [JsonProperty("detail")]
        public string Errors { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("status")]
        public HttpStatusCode Status { get; set; }

        [JsonProperty("instance")]
        public string Instance { get; set; }
    }
}