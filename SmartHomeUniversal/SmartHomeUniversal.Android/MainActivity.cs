using System;
using Android;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using SmartHomeUniversal.Business;
using SmartHomeUniversal.Droid.Business;

namespace SmartHomeUniversal.Droid
{
    [Activity(Label = "SmartHomeUniversal", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        static readonly int REQUEST_CAMERA = 0;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            FreshMvvm.FreshIOC.Container.Register<IWifiFacade, AndroidWifiFacade>().AsSingleton();
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        void RequestCameraPermission()
        {
        }
    }
}

