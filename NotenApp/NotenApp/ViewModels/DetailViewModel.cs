using MvvmHelpers;
using NotenApp.Logic;
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
        public ObservableRangeCollection<HJNote> LKNoten { get; set; }
        public ObservableRangeCollection<HJNote> KlausurNoten { get; set; }
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
        
        public async Task Initialize(HjFach fach)
        {
            LKNoten.Clear();
            KlausurNoten.Clear();
            Ziel ziel = await FachService.GetFachZiel(fach);
            
            
            if(ziel != null)
            {
                Ziel = ziel.ZielNote.ToString();
                
            }
            else
            {
                Ziel = "-";
            }
            
            var lKNoten = await FachService.GetFachNoten(fach, NotenTyp.LK);
            LKNoten.AddRange(lKNoten);
            var klausurNoten = await FachService.GetFachNoten(fach, NotenTyp.Klausur);
            KlausurNoten.AddRange(klausurNoten);

        }
    }
}
