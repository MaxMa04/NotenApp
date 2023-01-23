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
    public class MainPageViewModel : INotifyPropertyChanged
    {
        HalbjahrViewModel halbjahrViewModel = new HalbjahrViewModel();
        private float? durchschnittHJ1;
        public float? DurchschnittHJ1
        {
            get => durchschnittHJ1;
            set
            {
                durchschnittHJ1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DurchschnittHJ1)));
            }
        }
        private float? durchschnittHJ2;
        public float? DurchschnittHJ2
        {
            get => durchschnittHJ2;
            set
            {
                durchschnittHJ2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DurchschnittHJ2)));
            }
        }
        private float? durchschnittHJ3;
        public float? DurchschnittHJ3
        {
            get => durchschnittHJ3;
            set
            {
                durchschnittHJ3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DurchschnittHJ3)));
            }
        }
        private float? durchschnittHJ4;
        public float? DurchschnittHJ4
        {
            get => durchschnittHJ4;
            set
            {
                durchschnittHJ4 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DurchschnittHJ4)));
            }
        }
        private string punktzahlBlock1;
        public string PunktzahlBlock1
        {
            get => punktzahlBlock1;
            set
            {
                punktzahlBlock1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PunktzahlBlock1)));
            }
        }
        private string punktzahlBlock2;
        public string PunktzahlBlock2
        {
            get => punktzahlBlock2;
            set
            {
                punktzahlBlock2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PunktzahlBlock2)));
            }
        }
        private float? abiturNote;
        public float? AbiturNote
        {
            get => abiturNote;
            set
            {
                abiturNote = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AbiturNote)));
            }
        }
        private float? durchschnittBlock2;
        public float? DurchschnittBlock2
        {
            get => durchschnittBlock2;
            set
            {
                durchschnittBlock2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DurchschnittBlock2)));
            }
        }
        public ObservableRangeCollection<Ziel> FachZiele { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public MainPageViewModel()
        {
            FachZiele = new ObservableRangeCollection<Ziel>();
            


        }
        public async Task GetPunktzahlen()
        {
            int punktzahlBlock1 = (int)await halbjahrViewModel.GetPunktzahlBlock1();
            int punktzahlBlock2 = await FachService.GetPunktzahlBlock2();


            if (punktzahlBlock1 < 5)
            {
                PunktzahlBlock1 = "-/600";
            }
            else
            {
                PunktzahlBlock1 = punktzahlBlock1.ToString() + "/600";
            }
            DurchschnittBlock2 = await FachService.GetDurchschnittBlock2();
            if (punktzahlBlock2 == 0)
            {
                PunktzahlBlock2 = "-/300";
            }
            else
            {
                PunktzahlBlock2 = punktzahlBlock2.ToString() + "/300";
            }
        }
        public async Task InitializeZiele()
        {
            FachZiele.Clear();
            List<Ziel> ziele = await FachService.GetZiele();
            FachZiele.AddRange(ziele);
        }

    }
}
