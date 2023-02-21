using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NotenApp.Models
{
    //Halbjahresnoten
    public class HJNote : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        private int note;
        public int Note
        {
            get => note;

            set
            {
                note = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Note)));
            }
        }
        public int Typ { get; set; }
        public int FachId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
