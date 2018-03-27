using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Model;
using Xunit;

namespace SmartHome.Tests.ClientTests
{
    [Collection("Client tests")]
    public class SonOffClientTests : ClientBaseTests
    {
        public SonOffClientTests(ClientFixture clientFixture) : base(clientFixture)
        {
        }

        [Fact]
        public async Task Login_WithUsername_Succesfull()
        {
            await Client.SOnOff().Login(new LoginRequest
            {
                Username = "ZoroZ",
                Password = "12345678"
            });
        }
    }
}
