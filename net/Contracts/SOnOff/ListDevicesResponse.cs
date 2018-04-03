using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SmartHome.Contracts.SOnOff
{
    public class ListDevicesResponse
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("devicelist")]
        public List<Device> Device { get; set; }
    }
}
