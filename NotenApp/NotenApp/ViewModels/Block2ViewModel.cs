using NotenApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NotenApp.ViewModels
{
    public class Block2ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string p1Name;
        public string P1Name
        {
            get => p1Name;
            set
            {
                p1Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(P1Name)));
            }
        }
        private int? p1NoteM;
        public int? P1NoteM
        {
            get => p1NoteM;
            set
            {
                p1NoteM = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(P1NoteM)));
            }
        }
        private int? p1NoteS;
        public int? P1NoteS
        {
            get => p1NoteS;
            set
            {
                p1NoteS = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(P1NoteS)));
            }
        }

        private string p2Name;
        public string P2Name
        {
            get => p2Name;
            set
            {
                p2Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(P2Name)));
            }
        }
        private int? p2NoteM;
        public int? P2NoteM
        {
            get => p2NoteM;
            set
            {
                p2NoteM = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(P2NoteM)));
            }
        }
        private int? p2NoteS;
        public int? P2NoteS
        {
            get => p2NoteS;
            set
            {
                p2NoteS = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(P2NoteS)));
            }
        }
        private string p3Name;
        public string P3Name
        {
            get => p3Name;
            set
            {
                p3Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(P3Name)));
            }
        }
        private int? p3NoteM;
        public int? P3NoteM
        {
            get => p3NoteM;
            set
            {
                p3NoteM = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(P3NoteM)));
            }
        }
        private int? p3NoteS;
        public int? P3NoteS
        {
            get => p3NoteS;
            set
            {
                p3NoteS = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(P3NoteS)));
            }
        }
        private string p4Name;
        public string P4Name
        {
            get => p4Name;
            set
            {
                p4Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(P4Name)));
            }
        }
        private int? p4NoteM;
        public int? P4NoteM
        {
            get => p4NoteM;
            set
            {
                p4NoteM = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(P4NoteM)));
            }
        }
        private string p5Name;
        public string P5Name
        {
            get => p5Name;
            set
            {
                p5Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(P5Name)));
            }
        }
        private int? p5NoteM;
        public int? P5NoteM
        {
            get => p5NoteM;
            set
            {
                p5NoteM = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(P3NoteM)));
            }
        }


    }
}
