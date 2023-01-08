using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NotenApp.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
