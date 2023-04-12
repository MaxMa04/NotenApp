
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
        
        public Abi()
        {
            InitializeComponent();
            
            


        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
           

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