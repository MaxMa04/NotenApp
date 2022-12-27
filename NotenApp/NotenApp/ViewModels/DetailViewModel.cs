﻿using MvvmHelpers;
using NotenApp.Models;
using NotenApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace NotenApp.ViewModels
{
    public  class DetailViewModel : INotifyPropertyChanged
    {
        public ObservableRangeCollection<HjNote> LKNoten { get; set; }
        public ObservableRangeCollection<HjNote> KlausurNoten { get; set; }
        public string FachName { get; set; }
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
        public event PropertyChangedEventHandler PropertyChanged;
        public DetailViewModel()
        {
            LKNoten = new ObservableRangeCollection<HjNote>();
            KlausurNoten = new ObservableRangeCollection<HjNote>();
        }
        
        public async Task Initialize(HjFach fach)
        {
            LKNoten.Clear();
            KlausurNoten.Clear();
            List<HjNote> gesamtNoten = (List<HjNote>)await FachService.GetNoten(fach.Halbjahr);
            List<HjNote> Lk = new List<HjNote>();
            List<HjNote> Kla = new List<HjNote>();
            foreach (var note in gesamtNoten)
            {
                if(note.Fach == fach.Name)
                {
                    switch (note.Type)
                    {
                        case 1:
                            Lk.Add(note);
                            break;
                        case 2:
                            Kla.Add(note);
                            break;
                    }
                }
            }
            LKNoten.AddRange(Lk);
            KlausurNoten.AddRange(Kla);
        }
    }
}
