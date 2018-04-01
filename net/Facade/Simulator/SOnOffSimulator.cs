using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartHome.Contracts.SOnOff;

namespace SmartHome.Facade.Simulator
{
    [GiveMeName("SOnOff")]
    public class SOnOffSimulator : SimulatorBase, ISOnOffFacade
    {
        public async Task<LoginResponse> Login(string userName, string password)
        {
            const string fileName = "Login.json";
            return Deserialize<LoginResponse>(fileName, userName);
        }
    }
}
