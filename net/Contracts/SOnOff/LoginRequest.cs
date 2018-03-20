using Newtonsoft.Json;

namespace SmartHome.Contracts.SOnOff
{

    public class LoginRequest
    {
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("appid")]
        public string Appid { get; set; }
        [JsonProperty("ts")]
        public int Ts { get; set; }
        [JsonProperty("nonce")]
        public string Nonce { get; set; }
        [JsonProperty("version")]
        public int Version { get; set; }
        [JsonProperty("model")]
        public string Model { get; set; }
        [JsonProperty("os")]
        public string Os { get; set; }
        [JsonProperty("imei")]
        public string Imei { get; set; }
        [JsonProperty("romVersion")]
        public string RomVersion { get; set; }
        [JsonProperty("apkVersion")]
        public string ApkVersion { get; set; }
    }

}