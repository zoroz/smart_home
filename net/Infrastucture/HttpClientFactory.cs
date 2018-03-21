using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmartHome.Infrastucture
{
    public class HttpClientFactory : IHttpClientFactory
    {
        private static IDictionary<string, HttpClient> _clients = new Dictionary<string, HttpClient>();

        public HttpClient Create(string baseUrl)
        {
            HttpClient client;

            if (_clients.TryGetValue(baseUrl, out client))
            {
                return client;
            }

            client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl),
            };

            _clients[baseUrl] = client;

            return client;
        }
    }
}
