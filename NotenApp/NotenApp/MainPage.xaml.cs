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
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.DataGrid;

namespace NotenApp
{
    public partial class MainPage : ContentPage
    {
        Block2ViewModel vm = new Block2ViewModel();
        


        public MainPage()
        {
            InitializeComponent();
            BindingContext = UserViewModel.Instance;
            Task.Run(async () => { await vm.InitBlock2(); });
            Task.Run(async () => await UserViewModel.Instance.InitZiele());
            Task.Run(async () => await Task.WhenAll(HalbjahrViewModel.Instance.LoadInitialFaecher(), HalbjahrViewModel.Instance.ChangeHjDurchschnitt(null)));

            
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await UserViewModel.Instance.InitUser();
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

        private async void GoToDetailsPage(object sender, SelectionChangedEventArgs e)
        {
           
            var ziel = e.CurrentSelection.FirstOrDefault() as Ziel;
            if (ziel != null)
            {
                var fach = await FachService.GetFach(ziel.FachName, ziel.Halbjahr);
                await Navigation.PushAsync(new DetailSeite(fach));
            }
            else
            {
                return;
            }
            cv.SelectedItem = null;
        }

        private void OpenAbiturInfoPopup(object sender, EventArgs e)
        {
            Navigation.ShowPopup(new AbiturInfoPopup()); 
        }

    }
}
