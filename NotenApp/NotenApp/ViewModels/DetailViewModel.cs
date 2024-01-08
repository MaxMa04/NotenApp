using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using NotenApp.Logic;
using NotenApp.Models;
using NotenApp.Pages;
using NotenApp.Services;
using Switch;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace NotenApp.ViewModels
{
    public  partial class DetailViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        public CustomSwitch Switch { get; set; }
        [ObservableProperty]
        HjFach fach;
        [ObservableProperty]
        Ziel ziel;
        public DetailViewModel()
        {
           
        }
        public async Task InitDetails(HjFach fach)
        {
            Fach = fach;
            Ziel = await FachService.GetFachZiel(Fach);
        }
        [RelayCommand]
        async Task AddNote()
        {
            NotenTyp? notenTyp = (NotenTyp?)await Application.Current.MainPage.Navigation.ShowPopupAsync(new EntscheidungsPopup(Fach.Name));
            int? note = null;

            if (notenTyp != null)
            {
                note = (int?)await Application.Current.MainPage.Navigation.ShowPopupAsync(new NotenPopup(WhichNote.Block1, (NotenTyp)notenTyp, Fach.Name));
            }

            if (notenTyp != null && note != null)
            {
                HJNote note2 = new HJNote { FachId = Fach.Id, Note = (int)note, Typ = (NotenTyp)notenTyp };
                await FachService.AddNote(note2);
                switch (note2.Typ)
                {
                    case NotenTyp.LK:
                        Fach.LKNoten.Add(note2);
                        break;
                    case NotenTyp.Klausur:
                        Fach.KlausurNoten.Add(note2);
                        break;
                    default:
                        break;
                }
                Fach.Durchschnitt = (float?)BerechneDurchschnitt();
          
                await FachService.UpdateFach(Fach);

            }
            else
            {
                return;
            }
            
            //Task[] tasks = new Task[] { model.InitNoten(fach), model.InitFachDurchschnitt(fach), model.InitEinzHj(fach)};
            // await Task.WhenAll(tasks);
            await HalbjahrViewModel.Instance.ChangeHjDurchschnitt(Fach.Halbjahr);
            await Task.WhenAll(FachService.UpdateUserB1(), UserViewModel.Instance.InitZiele());
        }
        [RelayCommand]
        async Task DeleteNote(HJNote note)
        {
            
            var user = await FachService.GetUserData();
            bool delete = true;
            if (user.ShowPopupWhenDeletingNote)
            {
                delete = (bool)await Application.Current.MainPage.Navigation.ShowPopupAsync(new DeleteNotePopup());
            }

            if (delete)
            {
                switch (note.Typ)
                {
                    case NotenTyp.LK:
                        Fach.LKNoten.Remove(note);
                        break;
                    case NotenTyp.Klausur:
                        Fach.KlausurNoten.Remove(note);
                        break;
                    default:
                        break;
                }
                Fach.Durchschnitt = (float?)BerechneDurchschnitt();
                await FachService.RemoveSingleNote(note);
                await FachService.UpdateFach(Fach);
                await HalbjahrViewModel.Instance.ChangeHjDurchschnitt(Fach.Halbjahr);
                await Task.WhenAll(FachService.UpdateUserB1(), UserViewModel.Instance.InitZiele());
            }
           
        }
        [RelayCommand]
        async Task ManageZiel()
        {
            int? note = (int?)await Application.Current.MainPage.Navigation.ShowPopupAsync(new NotenPopup(WhichNote.Ziel, NotenTyp.Ziel, Fach.Name));
            switch (note)
            {
                case null:
                    return;
                  
                case -1:
                    if(Ziel == null) return;
                    await FachService.DeleteZiel(Ziel);
                    Ziel = null;
                    break;
                default:
                    Ziel = await FachService.AddZiel(Fach, note);
                    break;
            }

            //await model.InitZiel(fach);
            await UserViewModel.Instance.InitZiele();
        }
        [RelayCommand]
        async Task SetEndnote()
        {
            Fach.Endnote = (int?)await Application.Current.MainPage.Navigation.ShowPopupAsync(new NotenPopup(WhichNote.Endnote, NotenTyp.Endnote, Fach.Name));
            if (Fach.Endnote != null)
            {
                Fach.Durchschnitt = Fach.Endnote;
                
            }
            else
            {
                Fach.Durchschnitt = (float?)BerechneDurchschnitt();
       
            }
            await FachService.UpdateFach(Fach);
            await HalbjahrViewModel.Instance.ChangeHjDurchschnitt(Fach.Halbjahr);
            await Task.WhenAll(FachService.UpdateUserB1(), UserViewModel.Instance.InitZiele());
        }
        public async Task HandleSwitch(HjFach fach, bool isToggled)
        {
            
            PrFach p1 = await FachService.GetPrFach(1);
            PrFach p2 = await FachService.GetPrFach(2);
            if (isToggled == true)
            {
                await FachService.UpdateFachState(fach, "LK");
                if (p1 == null)
                {
                    await FachService.AddPrFach(fach.Name, 1);
                }
                else if (p1.Name == "-" || p1.Name == fach.Name)
                {
                    await FachService.UpdateName(fach.Name, 1);
                }
                else if (p2 == null)
                {
                    await FachService.AddPrFach(fach.Name, 2);
                }
                else if (p2.Name == "-" || p2.Name == fach.Name)
                {
                    await FachService.UpdateName(fach.Name, 2);
                }
                else
                {
                    Console.WriteLine("Fehler");
                }
                //await InitEinzHj(fach);



            }
            else
            {
                await FachService.UpdateFachState(fach, "GK");
                if (p1.Name == fach.Name)
                {
                    await FachService.UpdateName("-", 1);
                }
                else if (p2.Name == fach.Name)
                {
                    await FachService.UpdateName("-", 2);
                }
                else
                {
                    Console.WriteLine("Fehler");
                }
                //await InitEinzHj(fach);


            }


        }
        public double? BerechneDurchschnitt()
        {
            // Überprüfen, ob beide Sammlungen null sind oder keine Elemente enthalten
            if ((Fach.LKNoten == null || Fach.LKNoten.Count == 0) && (Fach.KlausurNoten == null || Fach.KlausurNoten.Count == 0))
            {
                return null;
            }

            double lkDurchschnitt = BerechneDurchschnittFürSammlung(Fach.LKNoten);
            double klausurDurchschnitt = BerechneDurchschnittFürSammlung(Fach.KlausurNoten);

            int anzahlSammlungen = 0;
            if (Fach.LKNoten != null && Fach.LKNoten.Count > 0) anzahlSammlungen++;
            if (Fach.KlausurNoten != null && Fach.KlausurNoten.Count > 0) anzahlSammlungen++;

            // Vermeidet Division durch Null, falls anzahlSammlungen 0 ist
            if (anzahlSammlungen == 0)
            {
                return null;
            }

            return (lkDurchschnitt + klausurDurchschnitt) / anzahlSammlungen;
        }

        private double BerechneDurchschnittFürSammlung(ObservableRangeCollection<HJNote> noten)
        {
            if (noten == null || noten.Count == 0)
                return 0;

            double summe = 0;
            foreach (var note in noten)
            {
                summe += note.Note; // Angenommen, HJNote hat eine Eigenschaft 'Wert'
            }
            return summe / noten.Count;
        }

    }
}
