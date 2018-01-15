using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using FreshMvvm;
using SmartHomeUniversal.Business;
using SmartHomeUniversal.Models;
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

        public WifiDevice SelectedItem { get; set; }

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
