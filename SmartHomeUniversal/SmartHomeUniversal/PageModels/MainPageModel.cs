using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net.Http;
using FreshMvvm;
using Newtonsoft.Json;
using SmartHomeUniversal.Business;
using SmartHomeUniversal.Models;
using SmartHomeUniversal.Models.SOnOff;
using Xamarin.Forms;

namespace SmartHomeUniversal.PageModels
{
    public class MainPageModel : FreshBasePageModel
    {
        private readonly IWifiFacade _wifiFacade;

        public ObservableCollection<WifiDevice> Items { get; } = new ObservableCollection<WifiDevice>();

        public MainPageModel(IWifiFacade wifiFacade)
        {
            _wifiFacade = wifiFacade;
        }

        public Command ScanCommand
        {
            get
            {
                return new Command(() => {
                    _wifiFacade.List();
                    }
                );
            }
        }

        public Command ConnectCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (SelectedItem == null)
                        return;

                    _wifiFacade.Connect(SelectedItem);
                });
            }
        }

        private WifiDevice _selectedItem;
        public WifiDevice SelectedItem {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                _wifiFacade.Connect(value);

                Items.Clear();

                try
                {
                    HttpClient c = new HttpClient();
                    var t = c.GetAsync("http://10.10.7.1/device").Result;
                    string res = t.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(res);

                    DeviceInfo device = JsonConvert.DeserializeObject<DeviceInfo>(res);

                    c.PostAsync("http://10.10.7.1/ap",
                        new StringContent(JsonConvert.SerializeObject(new ConfigureDevice()
                        {
                            Password = "ActiveBit",
                            Ssid = "asus",
                            Version = 4,
                            Port = 80,
                            ServerName = "192.168.1.10"
                        })));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            _wifiFacade.Available.CollectionChanged += (sender, args) =>
            {
                if (args.Action != NotifyCollectionChangedAction.Add)
                    return;

                foreach (WifiDevice device in args.NewItems)
                {
                    Items.Add(device);
                }
            };

        }
    }
}
