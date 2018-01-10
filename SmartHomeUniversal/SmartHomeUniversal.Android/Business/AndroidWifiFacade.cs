using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
        private TaskCompletionSource<IList<ScanResult>> _tcs;
        public ObservableCollection<WifiDevice> Available { get; } = new ObservableCollection<WifiDevice>();

        public AndroidWifiFacade()
        {
            _context = Application.Context;
            _wifiManager = (WifiManager)_context.GetSystemService(Context.WifiService);
        }

        public async Task<List<WifiDevice>> List()
        {
            WifiReceiver wifiReceiver = new WifiReceiver();

            _context.RegisterReceiver(wifiReceiver, new IntentFilter(WifiManager.ScanResultsAvailableAction));
            _wifiManager.StartScan();

            //var res = await _tcs.Task;
            //return res.Select(i => new WifiDevice() { Name = i.Ssid }).ToList();
            return new List<WifiDevice>();
        }

        class WifiReceiver : BroadcastReceiver
        {
            public override void OnReceive(Context context, Intent intent)
            {
                var wifiManager = (WifiManager)context.GetSystemService(Context.WifiService);
                var message = string.Join("\r\n", wifiManager.ScanResults
                    .Select(r => $"{r.Bssid} - {r.Level} dB"));
                
                    Debug.WriteLine($"Wifis found:[{message}]");
                
            }
        }
    }
}