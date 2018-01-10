using System.Collections.Generic;
using FreshMvvm;
using SmartHomeUniversal.Business;
using SmartHomeUniversal.Models;
using Xamarin.Forms;

namespace SmartHomeUniversal.PageModels
{
    public class MainPageModel : FreshBasePageModel
    {
        private readonly IWifiFacade _wifiFacade;

        public List<WifiDevice> Items { get; } = new List<WifiDevice>();

        public MainPageModel(IWifiFacade wifiFacade)
        {
            _wifiFacade = wifiFacade;
        }

        public Command ScanCommand
        {
            get
            {
                return new Command(async () => {
                        await _wifiFacade.List();
                    }
                );
            }
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            //var res = _wifiFacade.List().Result;
            //Items.AddRange(res);
        }
    }
}
