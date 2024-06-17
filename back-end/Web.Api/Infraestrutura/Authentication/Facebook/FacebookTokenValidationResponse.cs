using Newtonsoft.Json;

namespace Web.Api.Infraestrutura.Authentication.Facebook
{
    public class FacebookTokenValidationResponse
    {
        [JsonProperty("data")]
        public FacebookTokenValidationData Data { get; set; }
    }
}
