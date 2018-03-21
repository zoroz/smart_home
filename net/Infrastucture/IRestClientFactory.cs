using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Infrastucture
{
    public interface IRestClientFactory
    {
        IRestClient Create(string endpoint);
    }
}
