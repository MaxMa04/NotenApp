﻿using NotenApp.Models;
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
    public partial class Halbjahr3 : ContentPage
    {
        HalbjahrViewModel _model;
        public Halbjahr3()
        {
            InitializeComponent();
            _model = BindingContext as HalbjahrViewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Task.Run(async () => { await _model.Refresh(3); });
        }



        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var fach = e.CurrentSelection.FirstOrDefault() as HjFach;
            Navigation.ShowPopup(new EntscheidungsSeite(fach));
        }



        private async void SwipeItem_Invoked_1(object sender, EventArgs e)
        {
            SwipeItem swipeItem = sender as SwipeItem;
            var selectedItem = swipeItem.BindingContext as HjFach;
            await Navigation.PushAsync(new DetailSeite(selectedItem));
        }
    }
}