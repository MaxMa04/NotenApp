﻿using NotenApp.Models;
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
    public partial class NotenSeite : ContentPage
    {
        private Fach _fach;
        private int _entscheidung;
        Halbjahr1ViewModel viewModel;
        public NotenSeite(Fach fach, int entscheidung)
        {
            InitializeComponent();
            _fach = fach;
            _entscheidung = entscheidung;
            viewModel = new Halbjahr1ViewModel();   
        }

        private async void Button_Clicked1(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int note = Convert.ToInt32(button.Text);

            if(_entscheidung == 1)
            {
                await viewModel.AddNote(_fach, note);

                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

                await Navigation.PopAsync();
            }

        }
    }
}