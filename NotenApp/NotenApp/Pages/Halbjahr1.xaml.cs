﻿using MvvmHelpers;
using NotenApp.Models;
using NotenApp.Services;
using NotenApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotenApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Halbjahr1 : ContentPage
    {
        HalbjahrViewModel _model;
 
        public Halbjahr1()
        {
            
            InitializeComponent();
            _model = BindingContext as HalbjahrViewModel;
            
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Task.Run( async () => { await _model.Refresh(1); });

        }


        private async void FachHinzufuegen(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FachHinzufuegenSeite());
        }


        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var fach = e.CurrentSelection.FirstOrDefault() as HjFach;
            await Navigation.PushAsync(new EntscheidungsSeite(fach));
        }



        private async void SwipeItem_Invoked_1(object sender, EventArgs e)
        {
            SwipeItem swipeItem = sender as SwipeItem;
            var selectedItem = swipeItem.BindingContext as HjFach;
            await Navigation.PushAsync(new DetailSeite(selectedItem));
        }
    }
}