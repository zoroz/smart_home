using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace SmartHome.Tests.ClientTests
{
    public class ClientFixture : IDisposable
    {
        public ClientFixture()
        {
            Server = new TestServer(
                new WebHostBuilder()
                .UseStartup<Startup>()
                .UseSetting("SimulatorMode", "true"));
        }

        public TestServer Server { get; }

        public void Dispose()
        {
            Server?.Dispose();
        }
    }
}
