﻿using MvvmHelpers;
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
    public partial class DetailSeite : ContentPage
    {
        public FachModel fach;
        DetailViewModel model;
        public float heightCollView;
        public const float heightNote = 80; //Höhe der einzelnen Noten in der Übersicht
        public DetailSeite(FachModel fach)
        {
            InitializeComponent();
            this.fach = fach;
            Label.Text = fach.Name;
            model = BindingContext as DetailViewModel;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await model.Initialize(fach);
            model.FachDurchschnitt = (float)await FachService.GetFachDurchschnitt(fach);
            //Sizing Collection View for LK Noten

            if(model.LKNoten.Count == 0)
            {
                cv.HeightRequest = 1;
            }
            else if (model.LKNoten.Count < 7 && model.LKNoten.Count > 0)
            {
                cv.HeightRequest = heightNote;
            }
            else
            {
                heightCollView = model.LKNoten.Count / 6;
                cv.HeightRequest = heightNote + heightNote * (int)heightCollView;
            }
            
            
            rdcv.Height = cv.HeightRequest;

        }



    }
}