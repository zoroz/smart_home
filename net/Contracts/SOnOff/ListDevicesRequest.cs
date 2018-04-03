using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Contracts.SOnOff
{
    public class ListDevicesRequest
    {
        public string AppId { get; set; }

        public string Nonce { get; set; }

        public string Os { get; set; }

        public string Ts { get; set; }

        public int Version { get; set; }
    }
}
