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
        MainPageViewModel model;
        DetailSeite detailSeite;
        


        public MainPage()
        {
            InitializeComponent();
            model = BindingContext as MainPageViewModel;
            

            Task.Run(async () =>
            {
                await HalbjahrViewModel.Instance.Refresh(1);
                await HalbjahrViewModel.Instance.ChangeHjDurchschnitt(1);
                await HalbjahrViewModel.Instance.Refresh(2);
                await HalbjahrViewModel.Instance.ChangeHjDurchschnitt(2);
                await HalbjahrViewModel.Instance.Refresh(3);
                await HalbjahrViewModel.Instance.ChangeHjDurchschnitt(3);
                await HalbjahrViewModel.Instance.Refresh(4);
                await HalbjahrViewModel.Instance.ChangeHjDurchschnitt(4);
            });
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Task.Run(async () =>
            {
                model.DurchschnittHJ1 = await FachService.GetHJGesamtDurchschnitt(1);
                model.DurchschnittHJ2 = await FachService.GetHJGesamtDurchschnitt(2);
                model.DurchschnittHJ3 = await FachService.GetHJGesamtDurchschnitt(3);
                model.DurchschnittHJ4 = await FachService.GetHJGesamtDurchschnitt(4);
            });
            Task.Run(async () =>
            {
                await model.InitializeZiele();
            });
            Task.Run(async () =>
            {
                await model.GetPunktzahlen();
            });
            Task.Run(async () =>
            {
                model.AbiturNote = await FachService.GetAbiturNote();
            });

        }

        private async void Tapped1(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Halbjahr1(), false);
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
            await Navigation.PushAsync(new Halbjahr4(), false);
        }
        private async void OpenBlock1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Halbjahresuebersicht(), false);
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
                await model.InitializeZiele();
            });

        }
    }
}
