using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SmartHomeUniversal.Models.SOnOff
{
    public class DeviceInfo
    {
        [JsonProperty("deviceid")]
        public string DeviceId { get; set; }

        [JsonProperty("apikey")]
        public string ApiKey { get; set; }

        [JsonProperty("accept")]
        public string Accept { get; set; }
    }
}
