using MvvmHelpers;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NotenApp.Models
{
    //Halbjahresfächer
    public class HjFach : BaseFach
    {
        public int Halbjahr { get; set; }
        public int MinHalbjahre { get; set; } //Mindestanzahl an Halbjahren, die eingebracht werden müssen
        public int EingebrachteHalbjahre { get; set; } //Anzahl an Halbjahren, die die Aufgrund von Einbringungsregeln eingebracht werden
        public int Aufgabenfeld { get; set; } 
        public bool IsLK { get; set; } //Ist das Fach dein Leistungskurs?
        public bool IsPrFach { get; set; }
        public bool IsFremdsprache { get; set; }
        [Ignore]
        public ObservableRangeCollection<HJNote> LKNoten { get; set; }
        [Ignore]
        public ObservableRangeCollection<HJNote> KlausurNoten { get; set; }
        public HjFach()
        {
            LKNoten = new ObservableRangeCollection<HJNote>();
            KlausurNoten = new ObservableRangeCollection<HJNote>();
        }
    }
}
