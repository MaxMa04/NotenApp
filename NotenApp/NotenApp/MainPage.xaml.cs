using NotenApp.Models;
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
        MainPageViewModel _model;
        public MainPage()
        {
            InitializeComponent();
            _model = BindingContext as MainPageViewModel;
            
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

        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ziel = e.CurrentSelection.FirstOrDefault() as Ziel;
            await FachService.DeleteZiel(ziel);
            await Task.Run(async () =>
            {
                await _model.Initialize();
            });

        }
    }
}
