using MvvmHelpers;
using NotenApp.Logic;
using NotenApp.Models;
using NotenApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotenApp.ViewModels
{
    public class FachHinzufuegenViewModel
    {
        public ObservableRangeCollection<HjFach> AllFaecher { get; set; }
        
        public ObservableRangeCollection<HjFach> HjFaecher { get; set; }
        public ObservableRangeCollection<HjFach> PrFaecher { get; set; }


        public FachHinzufuegenViewModel()
        {
            AllFaecher = new ObservableRangeCollection<HjFach>{
                new HjFach { Name = "Mathe", MinHalbjahre = 4, Aufgabenfeld = (int)FachAufgabenfeld.Naturwissenschaftlich, IsLK = false, IsPrFach = false, IsFremdsprache = false },
                new HjFach { Name = "Deutsch", MinHalbjahre = 4, Aufgabenfeld = (int)FachAufgabenfeld.Sprachlich, IsLK = false, IsPrFach = false, IsFremdsprache = false },
                new HjFach { Name = "Englisch", MinHalbjahre = 1, Aufgabenfeld = (int)FachAufgabenfeld.Sprachlich, IsLK = false, IsPrFach = false, IsFremdsprache = true },
                new HjFach { Name = "Physik", MinHalbjahre = 1, Aufgabenfeld = (int)FachAufgabenfeld.Naturwissenschaftlich, IsLK = false, IsPrFach = false, IsFremdsprache = false },
                new HjFach { Name = "G/R/W", MinHalbjahre = 1, Aufgabenfeld = (int)FachAufgabenfeld.Gesellschaftlich, IsLK = false, IsPrFach = false, IsFremdsprache = false },
                new HjFach { Name = "Chemie", MinHalbjahre = 1, Aufgabenfeld = (int)FachAufgabenfeld.Naturwissenschaftlich, IsLK = false, IsPrFach = false, IsFremdsprache = false },
                new HjFach { Name = "Ethik", MinHalbjahre = 2, Aufgabenfeld = (int)FachAufgabenfeld.Kein, IsLK = false, IsPrFach = false, IsFremdsprache = false },
                new HjFach { Name = "Religion", MinHalbjahre = 2, Aufgabenfeld = (int)FachAufgabenfeld.Kein, IsLK = false, IsPrFach = false, IsFremdsprache = false },
                new HjFach { Name = "Astronomie", MinHalbjahre = 1, Aufgabenfeld = (int)FachAufgabenfeld.Naturwissenschaftlich, IsLK = false, IsPrFach = false, IsFremdsprache = false },
                new HjFach { Name = "Informatik", MinHalbjahre = 1, Aufgabenfeld = (int)FachAufgabenfeld.Naturwissenschaftlich, IsLK = false, IsPrFach = false, IsFremdsprache = false },
                new HjFach { Name = "Biologie", MinHalbjahre = 1, Aufgabenfeld = (int)FachAufgabenfeld.Naturwissenschaftlich, IsLK = false, IsPrFach = false, IsFremdsprache = false },
                new HjFach { Name = "Geografie", MinHalbjahre = 1, Aufgabenfeld = (int)FachAufgabenfeld.Gesellschaftlich, IsLK = false, IsPrFach = false, IsFremdsprache = false },
                new HjFach { Name = "Sport", MinHalbjahre = 1, Aufgabenfeld = (int)FachAufgabenfeld.Kein, IsLK = false, IsPrFach = false, IsFremdsprache = false },
                new HjFach { Name = "Musik", MinHalbjahre = 2, Aufgabenfeld = (int)FachAufgabenfeld.Sprachlich, IsLK = false, IsPrFach = false, IsFremdsprache = false },
                new HjFach { Name = "Kunst", MinHalbjahre = 2, Aufgabenfeld = (int)FachAufgabenfeld.Sprachlich, IsLK = false, IsPrFach = false, IsFremdsprache = false },
                new HjFach { Name = "Philosophie", MinHalbjahre = 1, Aufgabenfeld = (int)FachAufgabenfeld.Gesellschaftlich, IsLK = false, IsPrFach = false, IsFremdsprache = false },
                new HjFach { Name = "Geschichte", MinHalbjahre = 4, Aufgabenfeld = (int)FachAufgabenfeld.Gesellschaftlich, IsLK = false, IsPrFach = false, IsFremdsprache = false },
                new HjFach { Name = "Französich", MinHalbjahre = 1, Aufgabenfeld = (int)FachAufgabenfeld.Sprachlich, IsLK = false, IsPrFach = false, IsFremdsprache = true },
                new HjFach { Name = "Russisch", MinHalbjahre = 1, Aufgabenfeld = (int)FachAufgabenfeld.Sprachlich, IsLK = false, IsPrFach = false, IsFremdsprache = true },
                new HjFach { Name = "Latein", MinHalbjahre = 1, Aufgabenfeld = (int)FachAufgabenfeld.Sprachlich, IsLK = false, IsPrFach = false, IsFremdsprache = true },
                new HjFach { Name = "Sorbisch", MinHalbjahre = 1, Aufgabenfeld = (int)FachAufgabenfeld.Sprachlich, IsLK = false, IsPrFach = false, IsFremdsprache = true },
                new HjFach { Name = "Spanisch", MinHalbjahre = 1, Aufgabenfeld = (int)FachAufgabenfeld.Sprachlich, IsLK = false, IsPrFach = false, IsFremdsprache = true }
            };
            HjFaecher = new ObservableRangeCollection<HjFach>();
            PrFaecher = new ObservableRangeCollection<HjFach>();


        }
        public async Task InitHjFaecher()
        {
            HjFaecher.Clear();
            var facher2 = await FachService.GetFaecher(1);
            List<HjFach> hjFaecher = new List<HjFach>();
            foreach (var fach in AllFaecher)
            {
                if(facher2.Exists(f => f.Name == fach.Name) != true)
                {
                    hjFaecher.Add(fach);
                }

            }
            HjFaecher.AddRange(hjFaecher);
        }
            

        
        public async Task InitPrFaecher()
        {
            PrFaecher.Clear();
            var list = await FachService.GetFaecher(1);
            List<HjFach> prFaecher = new List<HjFach>();
            foreach (var fach in list)
            {
                if(fach.IsPrFach == false)
                {
                    prFaecher.Add(fach);
                }
            }
            PrFaecher.AddRange(prFaecher);
        }



    }
}
