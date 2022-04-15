using NotenApp.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NotenApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Halbjahresuebersicht());
        }
        private async void Button_Clicked1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Einstellungen());
        }
    }
}
