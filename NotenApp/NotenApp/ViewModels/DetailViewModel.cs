using MvvmHelpers;
using NotenApp.Logic;
using NotenApp.Models;
using NotenApp.Services;
using Switch;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NotenApp.ViewModels
{
    public  class DetailViewModel : INotifyPropertyChanged
    {
        public ObservableRangeCollection<HJNote> LKNoten { get; set; }
        public ObservableRangeCollection<HJNote> KlausurNoten { get; set; }
        public CustomSwitch Switch { get; set; }
        public HjFach Fach { get; set; }
        private string ziel;
        public string Ziel
        {
            get => ziel;
            set
            {
                ziel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Ziel)));
            }
        }

        private string fachName;
        public string FachName
        {
            get => fachName;
            set
            {
                fachName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FachName)));
            }
        }
    
        private float? fachDurchschnitt;
        public float? FachDurchschnitt
        {
            get => fachDurchschnitt;
            set
            {
                fachDurchschnitt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FachDurchschnitt)));
            }
        }
        private int fachEinzubringendeHalbjahre;
        public int FachEinzubringendeHalbjahre
        {
            get => fachEinzubringendeHalbjahre;
            set
            {
                fachEinzubringendeHalbjahre = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FachEinzubringendeHalbjahre)));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public DetailViewModel()
        {
            LKNoten = new ObservableRangeCollection<HJNote>();
            KlausurNoten = new ObservableRangeCollection<HJNote>();
        }
        public async Task InitZiel(HjFach fach)
        {
            Ziel ziel = await FachService.GetFachZiel(fach);


            if (ziel != null)
            {
                Ziel = ziel.ZielNote.ToString();

            }
            else
            {
                Ziel = "-";
            }
        }
        public async Task InitNoten(HjFach fach)
        {
            KlausurNoten.Clear();
            var klausurNoten = await FachService.GetFachNoten(fach, NotenTyp.Klausur);
            KlausurNoten.AddRange(klausurNoten);
            LKNoten.Clear();
            var lKNoten = await FachService.GetFachNoten(fach, NotenTyp.LK);
            LKNoten.AddRange(lKNoten);
        }
        public async Task InitFachDurchschnitt(HjFach fach)
        {
            FachDurchschnitt = await FachService.GetFachDurchschnitt(fach);
        }
        public async Task InitEinzHj(HjFach fach)
        {
            FachEinzubringendeHalbjahre = await FachService.GetEinzubringendeHalbjahre(fach);
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
                await InitEinzHj(fach);



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
                await InitEinzHj(fach);


            }


        }
    }
}
