using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SmartHomeUniversal.Models;

namespace SmartHomeUniversal.Business
{
    public interface IWifiFacade
    {
        ObservableCollection<WifiDevice> Available { get; }
        void List();
        void Connect(WifiDevice item);
    }
}
