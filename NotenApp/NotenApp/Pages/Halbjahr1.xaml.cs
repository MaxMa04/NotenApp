
using MvvmHelpers;
using NotenApp.Logic;
using NotenApp.Models;
using NotenApp.Services;
using NotenApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotenApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Halbjahr1 : ContentPage
    {
        public Halbjahr1()
        {
            InitializeComponent();
            BindingContext = HalbjahrViewModel.Instance;
            
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var user = await FachService.GetUserData();
            if (HalbjahrViewModel.Instance.FaecherHJ1.Any() && user.ShowFachHelpPopup)
            {
                Navigation.ShowPopup(new FachHelpPopup());
            }
        }
        private async void FachHinzufuegen(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FachHinzufuegenSeite());
            
        }
        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var fach = e.CurrentSelection.FirstOrDefault() as HjFach;
            if(fach == null)
            {
                return;
            }

            NotenTyp? notenTyp = (NotenTyp?)await Navigation.ShowPopupAsync(new EntscheidungsPopup(fach.Name));
            int? note = null;

            if (notenTyp != null)
            {
                 note = (int?)await Navigation.ShowPopupAsync(new NotenPopup(WhichNote.Block1, (NotenTyp)notenTyp, fach.Name));
            }
            
            if (notenTyp != null && note != null)
            {
                await HalbjahrViewModel.Instance.AddNote(fach, (int)note, (NotenTyp)notenTyp);
            }

            cv.SelectedItem = null;
            
        }
        private async void SwipeItem_Invoked_1(object sender, EventArgs e)
        {
            SwipeItem swipeItem = sender as SwipeItem;
            var selectedItem = swipeItem.BindingContext as HjFach;
            await Navigation.PushAsync(new DetailSeite(selectedItem));
        }
    }
}