using NotenApp.Services;
using System;
using Xamarin.Essentials;
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
            Settings.SetTheme(Settings.Theme);
            MainPage = new NavigationPage(new MainPage());
        }
        public App(string dB_Path)
        {
            InitializeComponent();
            Settings.SetTheme(Settings.Theme);
            DB_Path = dB_Path;
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            OnResume();
        }

        protected override void OnSleep()
        {
            Settings.SetTheme(Settings.Theme);
            RequestedThemeChanged -= App_RequestedThemeChanged;
        }

        protected override void OnResume()
        {
            Settings.SetTheme(Settings.Theme);
            RequestedThemeChanged += App_RequestedThemeChanged;
        }
        private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Settings.SetTheme(Settings.Theme);
            });
        }
    }
}
