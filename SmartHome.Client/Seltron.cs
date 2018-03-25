using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RestClient;
using RestClient.Attributes;
using SmartHome.Model;

namespace SmartHome.Client
{
    public class Seltron
    {
        private readonly IRestClient _client;

        public Seltron(IRestClient client)
        {
            _client = client;
        }

        [Get("Seltron")]
        public async Task Login(LoginRequest request)
        {
            await _client.SendAsync(request);
        }
    }
}
