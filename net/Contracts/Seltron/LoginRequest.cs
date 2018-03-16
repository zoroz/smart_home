using Newtonsoft.Json;

namespace SmartHome.Contracts.Seltron
{
    public class LoginRequest
    {
        [JsonProperty("realm")]
        public string Realm { get; set; }

        [JsonProperty("audience")]
        public string Audience { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("grant_type")]
        public string GrantType { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
