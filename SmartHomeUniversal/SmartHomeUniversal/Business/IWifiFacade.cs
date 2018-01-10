using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHomeUniversal.Models;

namespace SmartHomeUniversal.Business
{
    public interface IWifiFacade
    {
        Task<List<WifiDevice>> List();
    }
}
