using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NotenApp.Models
{
    public class BaseFach : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }
        private float? durchschnitt;
        public float? Durchschnitt
        {
            get => durchschnitt;
            
            set
            {
                durchschnitt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Durchschnitt)));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
