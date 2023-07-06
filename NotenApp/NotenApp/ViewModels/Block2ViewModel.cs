using MvvmHelpers.Commands;
using NotenApp.Models;
using NotenApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
        private float? durchschnittBlock2;
        public float? DurchschnittBlock2
        {
            get => durchschnittBlock2;
            set
            {
                durchschnittBlock2 = value;
                UserViewModel.Instance.DurchschnittBlock2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DurchschnittBlock2)));
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
        
        public async Task InitBlock2()
        {
            P1 = await FachService.GetPrFach(1);
            P2 = await FachService.GetPrFach(2);
            P3 = await FachService.GetPrFach(3);
            P4 = await FachService.GetPrFach(4);
            P5 = await FachService.GetPrFach(5);
            int? punktzahlBlock2 = await FachService.GetPunktzahlBlock2();
            DurchschnittBlock2 = await FachService.GetDurchschnittBlock2();

            if (punktzahlBlock2 == null)
            {
                PunktzahlBlock2 = "-/300";
            }
            else
            {
                PunktzahlBlock2 = punktzahlBlock2.ToString() + "/300";
            }
            await FachService.UpdateUserB2();
        }
        
    }
}
