using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartHome.Contracts.SOnOff;
using SmartHome.Infrastucture;
using SmartHome.Infrastucture.Attributes;

namespace SmartHome.Facade
{
    public interface ISOnOffFacade
    {
        Task<LoginResponse> Login(string userName, string password);
    }

    public class SOnOffFacade : ISOnOffFacade
    {
        private IRestClient _restClient;

        public SOnOffFacade(IRestClientFactory restClientFactory)
        {
            _restClient = restClientFactory.Create("https://api.coolkit.cc:8080/api");
        }

        [Post("user/login")]
        public async Task<LoginResponse> Login(string userName, string password)
        {
           return await _restClient.SendAsync<LoginResponse>(new LoginRequest
            {
                Appid = "oeVkj2lYFGnJu5XUtWisfW4utiN4u9Mq",
                Nonce = "u2omanuc",
                Os = "Android",
                Password = password,
                Ts = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds,
                Version = 6,
                Email = userName
            });
        }
    }

}
