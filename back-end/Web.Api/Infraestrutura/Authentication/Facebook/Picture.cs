using Newtonsoft.Json;

namespace Web.Api.Infraestrutura.Authentication.Facebook
{
    public class Picture
    {
        [JsonProperty("data")]
        public Data Data { get; set; }
    }
}
