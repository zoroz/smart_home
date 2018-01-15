using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Net.Wifi;
using SmartHomeUniversal.Business;
using SmartHomeUniversal.Models;

namespace SmartHomeUniversal.Droid.Business
{
    public class AndroidWifiFacade : IWifiFacade
    {
        private static WifiManager _wifiManager;
        private readonly Context _context;
        public ObservableCollection<WifiDevice> Available { get; } = new ObservableCollection<WifiDevice>();

        public AndroidWifiFacade()
        {
            _context = Application.Context;
            _wifiManager = (WifiManager)_context.GetSystemService(Context.WifiService);
        }

        public void List()
        {
            WifiReceiver wifiReceiver = new WifiReceiver(Available);

            _context.RegisterReceiver(wifiReceiver, new IntentFilter(WifiManager.ScanResultsAvailableAction));
            _wifiManager.StartScan();
        }

        public void Connect(WifiDevice item)
        {
            _wifiManager.AddNetwork(new WifiConfiguration
            {
                Ssid = item.Name,
                PreSharedKey = "\"12345678\""
            });

            var list = _wifiManager.ConfiguredNetworks;
            foreach (WifiConfiguration configuration in list)
            {
                if (configuration.Ssid != null && configuration.Ssid.Equals($"\"{item.Name}\""))
                {
                    _wifiManager.Disconnect();
                    _wifiManager.EnableNetwork(configuration.NetworkId, true);
                    _wifiManager.Reconnect();

                    break;
                }
            }
        }

        class WifiReceiver : BroadcastReceiver
        {
            private readonly ObservableCollection<WifiDevice> _available;

            public WifiReceiver(ObservableCollection<WifiDevice> available)
            {
                _available = available;
            }

            public override void OnReceive(Context context, Intent intent)
            {
                var wifiManager = (WifiManager)context.GetSystemService(Context.WifiService);
                foreach (ScanResult result in wifiManager.ScanResults)
                {
                    if(_available.All(i => i.Name != result.Ssid))
                        _available.Add(new WifiDevice() {Name = result.Ssid});
                }
            }
        }
    }
}