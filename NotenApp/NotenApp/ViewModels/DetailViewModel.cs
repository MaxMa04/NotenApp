using MvvmHelpers;
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
        private int einzhj;
        public int Einzhj
        {
            get => einzhj;
            set
            {
                einzhj = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Einzhj)));
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
                    switch (note.Typ)
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
