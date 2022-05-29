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
        private Halbjahr1Model fach1;
        private Halbjahr2Model fach2;
        private int _zahl;
        private int hjNummer;
        public EntscheidungsSeite(Halbjahr1Model fach)
        {
            InitializeComponent();
            fach1 = fach;
            hjNummer = 1;
        }
        public EntscheidungsSeite(Halbjahr2Model fach)
        {
            InitializeComponent();
            fach2 = fach;
            hjNummer = 2;
        }
        private async void LK_Button_Clicked(object sender, EventArgs e)
        {
            switch (hjNummer)
            {
                case 1:
                    _zahl = 1;
                    await Navigation.PushAsync(new NotenSeite(fach1, _zahl));
                    break;

                case 2:
                    _zahl = 1;
                    await Navigation.PushAsync(new NotenSeite(fach2, _zahl));
                    break;
            }
            
        }
        private async void Klausur_Button_Clicked(object sender, EventArgs e)
        {
            switch (hjNummer)
            {
                case 1:
                    _zahl = 2;
                    await Navigation.PushAsync(new NotenSeite(fach1, _zahl));
                    break;

                case 2:
                    _zahl = 2;
                    await Navigation.PushAsync(new NotenSeite(fach2, _zahl));
                    break;
            }
        }
    }
}