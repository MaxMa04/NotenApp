using NotenApp.Pages;
using NotenApp.Services;
using NotenApp.ViewModels;
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
        HalbjahrViewModel _model;
        public MainPage()
        {
            InitializeComponent();
            _model = BindingContext as HalbjahrViewModel;
            
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            _model.GesamtDurchschnittHJ1 = await FachService.GetHJGesamtDurchschnitt(1);
            _model.GesamtDurchschnittHJ2 = await FachService.GetHJGesamtDurchschnitt(2);
            _model.GesamtDurchschnittHJ3 = await FachService.GetHJGesamtDurchschnitt(3);
            _model.GesamtDurchschnittHJ4 = await FachService.GetHJGesamtDurchschnitt(4);
            _model.Bitte = await _model.GetPunktzahlBlock1();

        }
        private async void Tapped1(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Halbjahr1());
        }
        private async void Tapped2(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Halbjahr2());
        }
        private async void Tapped3(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Halbjahr3());
        }
        private async void Tapped4(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Halbjahr4());
        }
        private async void OpenBlock1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Halbjahresuebersicht());
        }
        private async void OpenSettings(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Einstellungen());
        }

        private async void OpenBlock2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Block2Page());
        }
    }
}
