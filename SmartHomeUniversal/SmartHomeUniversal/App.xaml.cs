using FreshMvvm;
using SmartHomeUniversal.PageModels;
using Xamarin.Forms;

namespace SmartHomeUniversal
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			MasterDetailNavigation();
		}

	    public void MasterDetailNavigation()
	    {
	        var masterDetailNav = new FreshMasterDetailNavigationContainer ();
            masterDetailNav.Init ("Menu", "Menu.png");
	        masterDetailNav.AddPage<MainPageModel> ("Home", null);
	        masterDetailNav.AddPage<TestPageModel> ("Test page", null);
	        MainPage = masterDetailNav;
	    }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
