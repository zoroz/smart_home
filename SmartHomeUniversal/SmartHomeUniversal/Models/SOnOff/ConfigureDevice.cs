using Newtonsoft.Json;

namespace SmartHomeUniversal.Models.SOnOff
{
    public class ConfigureDevice
    {
        [JsonProperty("version")]
        public int Version { get; set; }
        [JsonProperty("ssid")]
        public string Ssid { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("serverName")]
        public string ServerName { get; set; }
        [JsonProperty("port")]
        public int Port { get; set; }
    }
}
