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
        HalbjahrViewModel hjViewModel;
        public MainPage()
        {
            InitializeComponent();
            _model = BindingContext as MainPageViewModel;
            hjViewModel = new HalbjahrViewModel();
            
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            int punktzahlBlock1 = (int)await hjViewModel.GetPunktzahlBlock1();
            int punktzahlBlock2 = await FachService.GetPunktzahlBlock2();
            _model.DurchschnittHJ1 = await FachService.GetHJGesamtDurchschnitt(1);
            _model.DurchschnittHJ2 = await FachService.GetHJGesamtDurchschnitt(2);
            _model.DurchschnittHJ3 = await FachService.GetHJGesamtDurchschnitt(3);
            _model.DurchschnittHJ4 = await FachService.GetHJGesamtDurchschnitt(4);
            await _model.Initialize();
            if(punktzahlBlock1 < 5)
            {
                _model.PunktzahlBlock1 = "-/600";
            }
            else
            {
                _model.PunktzahlBlock1 = punktzahlBlock1.ToString() + "/600";
            }
            _model.DurchschnittBlock2 = await FachService.GetDurchschnittBlock2();
            if(punktzahlBlock2 == 0)
            {
                _model.PunktzahlBlock2 = "-/300";
            }
            else
            {
                _model.PunktzahlBlock2 = punktzahlBlock2.ToString() + "/300";
            }
            
            _model.AbiturNote = await FachService.GetAbiturNote();

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
            await _model.Initialize();
            
        }
    }
}
