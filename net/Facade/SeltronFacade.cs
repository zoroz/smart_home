using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartHome.Contracts.Seltron;

namespace SmartHome.Facade
{
    public interface ISeltronFacade
    {
        Task<LoginResponse> Login(string userName, string password);
    }

    public class SeltronFacade : ISeltronFacade
    {
        public async Task<LoginResponse> Login(string userName, string password)
        {
            HttpClient client = new HttpClient(new HttpClientHandler(){Proxy = new WebProxy("http://localhost:8888")});
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip, deflate"));
            client.DefaultRequestHeaders.Add("Auth0-Client", "eyJuYW1lIjoiYXV0aDAuanMiLCJ2ZXJzaW9uIjoiOC42LjEifQ==");
            HttpContent content = new StringContent(JsonConvert.SerializeObject(new LoginRequest
            {
                Audience = "https://api.seltronhome.com",
                ClientId = "lbO893m2FNTundKaTrRM00jTw5LTLMz2",
                GrantType = "http://auth0.com/oauth/grant-type/password-realm",
                Password = password,
                Realm = "Username-Password-Authentication",
                Scope= "openid offline_access",
                Username = userName
            }), Encoding.UTF8);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            content.Headers.Add("Origin", "file://");
            content.Headers.Add("X-Requested-With", "eu.seltron.clausius");

            HttpResponseMessage res = await client.PostAsync("https://seltronhome.eu.auth0.com/oauth/token", content);
            string data = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LoginResponse>(data);
        }
    }
}
