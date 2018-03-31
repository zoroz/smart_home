using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartHome.Contracts.Seltron;

namespace SmartHome.Facade.Simulator
{
    public class SeltronSimulator : SimulatorBase, ISeltronFacade
    {
        public Task<LoginResponse> Login(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}
