using NotenApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace NotenApp.ViewModels
{
    public class Block2ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private PrFach p1;
        public PrFach P1
        {
            get => p1;
            set
            {
                p1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(P1)));
            }
        }
        private PrFach p2;
        public PrFach P2
        {
            get => p2;
            set
            {
                p2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(P2)));
            }
        }
        private PrFach p3;
        public PrFach P3
        {
            get => p3;
            set
            {
                p3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(P3)));
            }
        }
        private PrFach p4;
        public PrFach P4
        {
            get => p4;
            set
            {
                p4 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(P4)));
            }
        }
        private PrFach p5;
        public PrFach P5
        {
            get => p5;
            set
            {
                p5 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(P5)));
            }
        }
        public Block2ViewModel() 
        { 
            
        }


    }
}
