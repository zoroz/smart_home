using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RestClient.Attributes;
using SmartHome.Contracts.SOnOff;

using SmartHome.Options;

namespace SmartHome.Facade
{
    public interface ISOnOffFacade
    {
        Task<LoginResponse> Login(string userName, string password);

        Task<List<Device>> ListDevices(string token);
    }

    public class SOnOffFacade : JsonHttpClient, ISOnOffFacade
    {
        public SOnOffFacade(IOptions<SOnOffHttpClientOptions> options) : base(options)
        {
        }

        [Post("user/login")]
        public async Task<LoginResponse> Login(string userName, string password)
        {
            return await SendAsync<LoginResponse>(new LoginRequest
            {
                Appid = "oeVkj2lYFGnJu5XUtWisfW4utiN4u9Mq",
                Nonce = "u2omanuc",
                Os = "Android",
                Password = password,
                Ts = (Int32) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds,
                Version = 6,
                Email = userName
            });
        }

        [Get("user/device")]
        public async Task<List<Device>> ListDevices(string token)
        {
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await SendAsync<List<Device>>(new ListDevicesRequest
            {
                AppId = "oeVkj2lYFGnJu5XUtWisfW4utiN4u9Mq",
                Nonce = "u2omanuc",
                Os = "Android",
                Version = 6,
                Ts = "1521569910",
            });
        }

        protected override void AddDefaultRequestHeaders(HttpRequestHeaders defaultRequestHeaders)
        {
            base.AddDefaultRequestHeaders(defaultRequestHeaders);

            if (defaultRequestHeaders.Authorization == null)
                defaultRequestHeaders.Authorization = new AuthenticationHeaderValue("sign", "qvpP9CHX/PUccoFpbpRzDPmFe2PVYZ2pATCP/3kzyMk=");
        }
    }

}
