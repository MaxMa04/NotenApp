using MvvmHelpers;
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
            model = BindingContext as DetailViewModel;
            model.FachName = fach.Name;



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
            model.FachEinzubringendeHalbjahre = await FachService.GetEinzubringendeHalbjahre(fach);

        }

        private async void DeleteNote(object sender, SelectionChangedEventArgs e)
        {
            var note = e.CurrentSelection.FirstOrDefault() as HJNote;
            await FachService.RemoveSingleNote(note);
            await model.Initialize(fach);
            model.FachDurchschnitt = await FachService.GetFachDurchschnitt(fach);
            model.FachEinzubringendeHalbjahre = await FachService.GetEinzubringendeHalbjahre(fach);
        }

        private async void AddKlausurNote(object sender, EventArgs e)
        {
            int? note = (int?)await Navigation.ShowPopupAsync(new NotenPopup(WhichNote.Block1));
            if(note != null)
            {
                await FachService.AddNote(fach, (int)note, NotenTyp.Klausur);
            }
            await model.Initialize(fach);
            model.FachDurchschnitt = await FachService.GetFachDurchschnitt(fach);
            model.FachEinzubringendeHalbjahre = await FachService.GetEinzubringendeHalbjahre(fach);

        }
        private async void AddLKNote(object sender, EventArgs e)
        {
            int? note = (int?)await Navigation.ShowPopupAsync(new NotenPopup(WhichNote.Block1));
            if (note != null)
            {
                await FachService.AddNote(fach, (int)note, NotenTyp.LK);
            }
            await model.Initialize(fach);
            model.FachDurchschnitt = await FachService.GetFachDurchschnitt(fach);
            model.FachEinzubringendeHalbjahre = await FachService.GetEinzubringendeHalbjahre(fach);
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            int? note = (int?)await Navigation.ShowPopupAsync(new NotenPopup(WhichNote.Ziel));
            
            await FachService.AddZiel(fach, note);
            await model.Initialize(fach);
          
        }
    }
}