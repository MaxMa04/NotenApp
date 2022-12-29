using MvvmHelpers;
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
        public HjFach fach;
        DetailViewModel model;
        public decimal heightCollView;
        public const float heightNote = 80; //Höhe der einzelnen Noten in der Übersicht
        public DetailSeite(HjFach fach)
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
            model.FachDurchschnitt = await FachService.GetFachDurchschnitt(fach);    
            //Sizing Collection View for LK Noten

            if(model.LKNoten.Count == 0)
            {
                cv.HeightRequest = 1;
            }
            else if (model.LKNoten.Count < 7)
            {
                cv.HeightRequest = heightNote;
            }
            else if (model.LKNoten.Count < 25)
            {
                heightCollView = model.LKNoten.Count / 6;
                cv.HeightRequest = heightNote + heightNote * (int)heightCollView - 15 * (int)heightCollView;
            }
            else
            {
                cv.HeightRequest = heightNote * 4 - 12 * 4;
            }
            
            rdcv.Height = cv.HeightRequest;

        }

        private async void DeleteNote(object sender, SelectionChangedEventArgs e)
        {
            var note = e.CurrentSelection.FirstOrDefault() as HjNote;
            await FachService.RemoveSingleNote(note);
            await model.Initialize(fach);
            model.FachDurchschnitt = await FachService.GetFachDurchschnitt(fach);
        }

        private async void AddKlausurNote(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotenSeite(fach, NotenTyp.Klausur,1));
        }
        private async void AddLKNote(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotenSeite(fach, NotenTyp.LK,1));
        }
    }
}