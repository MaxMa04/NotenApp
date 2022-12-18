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
        int? prFachNummer;
        public FachHinzufuegenSeite()
        {
            InitializeComponent();
        }
        public FachHinzufuegenSeite(int i)
        {
            InitializeComponent();
            prFachNummer = i;
        }

        private async void myListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var fach = e.SelectedItem as FachModel;

                await FachService.AddFach(fach.Name, 1);
                await FachService.AddFach(fach.Name, 2);
                await FachService.AddFach(fach.Name, 3);
                await FachService.AddFach(fach.Name, 4);
            
            await Navigation.PopAsync();
        }
    }
}