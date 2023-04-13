using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NotenApp.Models
{
    public class UserModel : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        private float? abischnitt;
        public float? Abischnitt
        {
            get => abischnitt;
            set
            {
                abischnitt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Abischnitt)));
            }
        }
        private int? punktzahlBlock1;
        public int? PunktzahlBlock1
        {
            get => punktzahlBlock1;
            set
            {
                punktzahlBlock1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PunktzahlBlock1)));
            }
        }
        private int? punktzahlBlock2;
        public int? PunktzahlBlock2
            {
                get => punktzahlBlock2;
            set
            {
                punktzahlBlock2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PunktzahlBlock2)));
            }
        }
        public bool ShowPopupWhenDeletingNote { get; set; } 
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
