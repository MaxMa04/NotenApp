using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotenApp.Models
{
    //Halbjahresfächer
    public class HjFach : BaseFach
    {
        public int Halbjahr { get; set; }
        public int MinHalbjahre { get; set; } //Mindestanzahl an Halbjahren, die eingebracht werden müssen
        public int EingebrachteHalbjahre { get; set; } //Anzahl, die eingebracht wird
        public int Aufgabenfeld { get; set; } 
        public bool IsLK { get; set; } //Ist das Fach dein Leistungskurs?
        public bool IsPrFach { get; set; }
        [Ignore]
        public List<HjNote> LKNoten { get; set; }
        [Ignore]
        public List<HjNote> KlausurNoten { get; set; }


        public HjFach()
        {
            LKNoten = new List<HjNote>();
            KlausurNoten = new List<HjNote>();
        }
    }
}
