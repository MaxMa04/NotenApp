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
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            model.FachName = fach.Name;
            Task.Run(async () => { await model.InitLks(fach); });
            Task.Run(async () => { await model.InitKlausuren(fach); });
            Task.Run(async () => { await model.InitEinzHj(fach); });
            Task.Run(async () => { await model.InitFachDurchschnitt(fach); });
            Task.Run(async () => { await model.InitZiel(fach); });

        }

        private async void DeleteNote(object sender, SelectionChangedEventArgs e)
        {
            var note = e.CurrentSelection.FirstOrDefault() as HJNote;
            await FachService.RemoveSingleNote(note);
            switch (note.Typ)
            {
                case (int)NotenTyp.LK:
                     await model.InitLks(fach);
                    break;
                case (int)NotenTyp.Klausur:
                    await model.InitKlausuren(fach);
                    break;
                
            }
            await model.InitFachDurchschnitt(fach);
            await model.InitEinzHj(fach);
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            int? note = (int?)await Navigation.ShowPopupAsync(new NotenPopup(WhichNote.Ziel));
            
            await FachService.AddZiel(fach, note);
            await model.InitZiel(fach);
          
        }

        private async void AddNote(object sender, EventArgs e)
        {
            NotenTyp? notenTyp = (NotenTyp?)await Navigation.ShowPopupAsync(new EntscheidungsPopup());
            int? note = null;

            if (notenTyp != null)
            {
                note = (int?)await Navigation.ShowPopupAsync(new NotenPopup(WhichNote.Block1));
            }

            if (notenTyp != null && note != null)
            {
                await FachService.AddNote(fach, (int)note, (NotenTyp)notenTyp);
                switch (notenTyp)
                {
                    case NotenTyp.LK:
                        await model.InitLks(fach);
                        break;
                    case NotenTyp.Klausur:
                        await model.InitKlausuren(fach);
                        break;

                }
            }

            await model.InitFachDurchschnitt(fach);
            await model.InitEinzHj(fach);
        }
    }
}