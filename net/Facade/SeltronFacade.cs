using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SmartHome.Contracts.Seltron;
using SmartHome.Infrastucture.Attributes;
using SmartHome.Options;


namespace SmartHome.Facade
{
    public interface ISeltronFacade
    {
        Task<LoginResponse> Login(string userName, string password);
    }

    public class SeltronFacade : JsonHttpClient, ISeltronFacade
    {
        public SeltronFacade(IOptions<SeltronHttpClientOptions> options) : base(options)
        {
        }

        [Post("oauth/token")]
        public async Task<LoginResponse> Login(string userName, string password)
        {
            return await SendAsync<LoginResponse>(new LoginRequest
            {
                Audience = "https://api.seltronhome.com",
                ClientId = "lbO893m2FNTundKaTrRM00jTw5LTLMz2",
                GrantType = "http://auth0.com/oauth/grant-type/password-realm",
                Password = password,
                Realm = "Username-Password-Authentication",
                Scope = "openid offline_access",
                Username = userName
            });
        }

        protected override void AddDefaultRequestHeaders(HttpRequestHeaders defaultRequestHeaders)
        {
            base.AddDefaultRequestHeaders(defaultRequestHeaders);
            defaultRequestHeaders.Add("Auth0-Client", "eyJuYW1lIjoiYXV0aDAuanMiLCJ2ZXJzaW9uIjoiOC42LjEifQ==");
        }
    }
}
