
using NotenApp.Services;
using NotenApp.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotenApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Abi : ContentPage
    {
        HalbjahrViewModel _model;
        public Abi()
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
    }
}