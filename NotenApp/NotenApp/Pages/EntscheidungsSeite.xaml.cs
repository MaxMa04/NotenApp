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
        private HjFach fach;

        public EntscheidungsSeite(HjFach fach)
        {
            InitializeComponent();
            this.fach = fach;
        }
        private async void LK_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotenSeite(fach, NotenTyp.LK, 2));           
        }
        private async void Klausur_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotenSeite(fach, NotenTyp.Klausur, 2));
        }
    }
}