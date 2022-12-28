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

        public Block2ViewModel() 
        { 
            
        }


    }
}
