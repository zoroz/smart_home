using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmartHome.Infrastucture
{
    public interface IHttpClientFactory
    {
        HttpClient Create(string baseUrl);
    }
}
