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

namespace SmartHome.Facade
{
    public interface ISOnOffFacade
    {
        Task<LoginResponse> Login(string userName, string password);
    }

    public class SOnOffFacade : ISOnOffFacade
    {
        public async Task<LoginResponse> Login(string userName, string password)
        {
            HttpClient client = new HttpClient(new HttpClientHandler { Proxy = new WebProxy("http://localhost:8888") });
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("sign", "qvpP9CHX/PUccoFpbpRzDPmFe2PVYZ2pATCP/3kzyMk=");
            //client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip, deflate"));
            //client.DefaultRequestHeaders.Add("Auth0-Client", "eyJuYW1lIjoiYXV0aDAuanMiLCJ2ZXJzaW9uIjoiOC42LjEifQ==");
            HttpContent content = new StringContent(JsonConvert.SerializeObject(new LoginRequest
            {
                Appid = "oeVkj2lYFGnJu5XUtWisfW4utiN4u9Mq",
                Nonce = "u2omanuc",
                Os = "Android",
                Password = password,
                Ts = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds,
                Version = 6,
                Email = userName
            }), Encoding.UTF8);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //content.Headers.Add("Origin", "file://");
            //content.Headers.Add("X-Requested-With", "eu.seltron.clausius");

            HttpResponseMessage res = await client.PostAsync("https://api.coolkit.cc:8080/api/user/login", content);
            string data = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LoginResponse>(data);
        }
    }

}
