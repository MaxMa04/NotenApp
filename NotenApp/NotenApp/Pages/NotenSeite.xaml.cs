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
        //private Halbjahr1Model _fach;
        private FachModel _fach2;
        private int _entscheidung;
        HalbjahrViewModel viewModel2;


        public NotenSeite(FachModel fach, int entscheidung)
        {
            InitializeComponent();
            _fach2 = fach;
            _entscheidung = entscheidung;
            viewModel2 = new HalbjahrViewModel();

        }

        private async void Button_Clicked1(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int note = Convert.ToInt32(button.Text);
            if (_entscheidung == 1)
            {
                await viewModel2.AddNote(_fach2, note, 1);

                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

                await Navigation.PopAsync();
            }
            else
            {
                await viewModel2.AddNote(_fach2, note, 2);

                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

                await Navigation.PopAsync();
            }




        }
    }
}