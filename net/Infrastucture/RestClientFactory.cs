using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Infrastucture
{
    public class RestClientFactory : IRestClientFactory
    {
        private static IDictionary<string, IRestClient> _clients = new Dictionary<string, IRestClient>();

        private readonly IHttpClientFactory _httpClientFactory;

        public RestClientFactory(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IRestClient Create(string endpoint)
        {
            IRestClient client;

            if (_clients.TryGetValue(endpoint, out client))
            {
                return client;
            }

            client = new RestClient(_httpClientFactory.Create(endpoint));
            _clients[endpoint] = client;

            return client;
        }
    }
}

