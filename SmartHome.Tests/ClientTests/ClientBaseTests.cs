using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.TestHost;
using SmartHome.Client;


namespace SmartHome.Tests.ClientTests
{
    public class ClientBaseTests
    {
        public ClientBaseTests(ClientFixture clientFixture)
        {
            var restClient = new RestClient.RestClient(clientFixture.Server.CreateHandler())
            {
                BaseAddress = clientFixture.Server.BaseAddress
            };

            Client = new WebApiClient(restClient);
            Server = clientFixture.Server;
        }

        protected TestServer Server { get; private set; }

        protected IWebApiClient Client { get; set; }
    }
}
