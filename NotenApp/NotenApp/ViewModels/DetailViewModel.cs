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
        public ObservableRangeCollection<NotenModel> LKNoten { get; set; }
        public ObservableRangeCollection<NotenModel> KlausurNoten { get; set; }
        public string FachName { get; set; }
        private float fachDurchschnitt;
        public float FachDurchschnitt
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
            LKNoten = new ObservableRangeCollection<NotenModel>();
            KlausurNoten = new ObservableRangeCollection<NotenModel>();
        }
        
        public async Task Initialize(FachModel fach)
        {
            
            List<NotenModel> gesamtNoten = (List<NotenModel>)await FachService.GetNoten(fach.Halbjahr);
            List<NotenModel> Lk = new List<NotenModel>();
            List<NotenModel> Kla = new List<NotenModel>();
            foreach (var item in gesamtNoten)
            {
                if(item.Fach == fach.Name)
                {
                    switch (item.Type)
                    {
                        case 1:
                            Lk.Add(item);
                            break;
                        case 2:
                            Kla.Add(item);
                            break;
                    }
                }
            }
            LKNoten.AddRange(Lk);
            KlausurNoten.AddRange(Kla);
        }
    }
}
