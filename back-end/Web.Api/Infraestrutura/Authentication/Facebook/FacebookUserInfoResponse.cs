using Newtonsoft.Json;

namespace Web.Api.Infraestrutura.Authentication.Facebook
{
    public class FacebookUserInfoResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("picture")]
        public Picture Picture { get; set; }
    }
}
