using System;
using Newtonsoft.Json;

namespace SmartHome.Contracts.SOnOff
{
    public class LoginResponse
    {
        [JsonProperty("at")]
        public string At { get; set; }

        [JsonProperty("rt")]
        public string Rt { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }
    }

    public class User
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("appId")]
        public string AppId { get; set; }

        [JsonProperty("online")]
        public bool Online { get; set; }

        [JsonProperty("onlineTime")]
        public DateTime OnlineTime { get; set; }

        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("offlineTime")]
        public DateTime OfflineTime { get; set; }

        [JsonProperty("userStatus")]
        public string UserStatus { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("apikey")]
        public string Apikey { get; set; }
    }
}