using Newtonsoft.Json;

namespace Web.Api.Infraestrutura.Authentication.Facebook
{
    public class Metadata
    {
        [JsonProperty("auth_type")]
        public string AuthType { get; set; }
    }
}
