﻿using NotenApp.Models;
using NotenApp.Services;
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
    public partial class FachHinzufuegenSeite : ContentPage
    {
        public FachHinzufuegenSeite()
        {
            InitializeComponent();
        }

        private async void myListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var fach = e.SelectedItem as Fach;

            await FachService.AddFach(fach.Name);
            await DisplayAlert(fach.Name, "wurde hinzugefügt", "Schließen");
            await Navigation.PopAsync();
        }
    }
}