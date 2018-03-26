using System.Threading.Tasks;
using RestClient;
using RestClient.Attributes;
using SmartHome.Model;

namespace SmartHome.Client
{
    public class SOnOff
    {
        private readonly IRestClient _client;

        public SOnOff(IRestClient client)
        {
            _client = client;
        }

        [Get("SonOff")]
        public async Task Login(LoginRequest request)
        {
           await _client.SendAsync(request);
        }
    }
}
