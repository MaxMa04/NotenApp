using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotenApp
{
    public partial class App : Application
    {
        public static string DB_Path = String.Empty;
        public App()
        {
            
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }
        public App(string dB_Path)
        {
            InitializeComponent();
            DB_Path = dB_Path;
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
