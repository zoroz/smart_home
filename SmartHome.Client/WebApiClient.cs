using System;
using RestClient;

namespace SmartHome.Client
{
    public class WebApiClient : IWebApiClient
    {
        private readonly IRestClient _client;

        public WebApiClient(IRestClient client)
        {
            _client = client;
        }


        public SOnOff SOnOff()
        {
            return new SOnOff(_client);
        }

        public Seltron Seltron()
        {
            return new Seltron(_client);
        }
    }
}
