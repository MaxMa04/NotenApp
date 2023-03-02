using NotenApp.Logic;
using NotenApp.Models;
using NotenApp.Services;
using NotenApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotenApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Halbjahr2 : ContentPage
    {
        HalbjahrViewModel _model;
        public Halbjahr2()
        {

            InitializeComponent();
            _model = BindingContext as HalbjahrViewModel;
            
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _model.Refresh(2);
            await _model.ChangeHjDurchschnitt(2);
        }




        private async void SwipeItem_Invoked_1(object sender, EventArgs e)
        {
            SwipeItem swipeItem = sender as SwipeItem;
            var selectedItem = swipeItem.BindingContext as HjFach;
            await Navigation.PushAsync(new DetailSeite(selectedItem));
        }

        private async void cv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var fach = e.CurrentSelection.FirstOrDefault() as HjFach;
            if (fach == null)
            {
                return;
            }

            NotenTyp? notenTyp = (NotenTyp?)await Navigation.ShowPopupAsync(new EntscheidungsPopup());
            int? note = null;

            if (notenTyp != null)
            {
                note = (int?)await Navigation.ShowPopupAsync(new NotenPopup(WhichNote.Block1));
            }

            if (notenTyp != null && note != null)
            {
                await _model.AddNote(fach, (int)note, (NotenTyp)notenTyp);
            }

            cv.SelectedItem = null;
        }
    }
}