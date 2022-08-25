using NotenApp.Models;
using NotenApp.Services;
using NotenApp.ViewModels;
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
            _model.GesamtDurchschnittHJ2 = await FachService.GetHJGesamtDurchschnitt(2);


        }

        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var fach = e.CurrentSelection.FirstOrDefault() as FachModel;
            await Navigation.PushAsync(new EntscheidungsSeite(fach));
        }



        private async void SwipeItem_Invoked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DetailSeite());
        }
    }
}