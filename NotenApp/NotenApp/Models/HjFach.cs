using CommunityToolkit.Mvvm.ComponentModel;
using MvvmHelpers;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NotenApp.Models
{
    //Halbjahresfächer
    public partial class HjFach : BaseFach
    {
        public int Halbjahr { get; set; }
        [ObservableProperty]
        private int minHalbjahre; //Mindestanzahl an Halbjahren, die eingebracht werden müssen
        [ObservableProperty]
        private int eingebrachteHalbjahre;//Anzahl an Halbjahren, die die Aufgrund von Einbringungsregeln eingebracht werden
        [ObservableProperty] private int aufgabenfeld;
        [ObservableProperty] private bool isLK; //Ist das Fach dein Leistungskurs?
        [ObservableProperty]private bool wirdEingebracht;
        [ObservableProperty]

        private int? endnote;
        [ObservableProperty]

        bool isPrFach;
        [ObservableProperty]

         bool isFremdsprache;
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
