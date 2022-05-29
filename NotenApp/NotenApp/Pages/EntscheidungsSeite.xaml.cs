using NotenApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotenApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntscheidungsSeite : ContentPage
    {
        private Halbjahr1Model _fach;
        private int _zahl;
        public EntscheidungsSeite(Halbjahr1Model fach)
        {
            InitializeComponent();
            _fach = fach;
        }

        private async void LK_Button_Clicked(object sender, EventArgs e)
        {
            _zahl = 1;
            await Navigation.PushAsync(new NotenSeite(_fach, _zahl));
        }
        private async void Klausur_Button_Clicked(object sender, EventArgs e)
        {
            _zahl = 2;
            await Navigation.PushAsync(new NotenSeite(_fach, _zahl));
        }
    }
}