using MvvmHelpers;
using NotenApp.Logic;
using NotenApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotenApp.ViewModels
{
    public class FachHinzufuegenViewModel
    {
        public ObservableRangeCollection<HjFach> Faecher { get; set; }
        public FachHinzufuegenViewModel()
        {
            Faecher = new ObservableRangeCollection<HjFach>
            {
                new HjFach { Name = "Mathe"      , MinHalbjahre=4, Aufgabenfeld=(int)FachAufgabenfeld.Naturwissenschaftlich, IsLK=false, IsPrFach=false     },
                new HjFach { Name = "Deutsch"    , MinHalbjahre=4, Aufgabenfeld=(int)FachAufgabenfeld.Sprachlich           , IsLK=false, IsPrFach=false     },
                new HjFach { Name = "Englisch"   , MinHalbjahre=4, Aufgabenfeld=(int)FachAufgabenfeld.Sprachlich           , IsLK=false, IsPrFach=false     },
                new HjFach { Name = "Physik"     , MinHalbjahre=1, Aufgabenfeld=(int)FachAufgabenfeld.Naturwissenschaftlich, IsLK=false, IsPrFach=false     },
                new HjFach { Name = "G/R/W"      , MinHalbjahre=2, Aufgabenfeld=(int)FachAufgabenfeld.Gesellschaftlich     , IsLK=false, IsPrFach=false     },
                new HjFach { Name = "Chemie"     , MinHalbjahre=1, Aufgabenfeld=(int)FachAufgabenfeld.Naturwissenschaftlich, IsLK=false, IsPrFach=false     },
                new HjFach { Name = "Ethik"      , MinHalbjahre=2, Aufgabenfeld=(int)FachAufgabenfeld.Kein                 , IsLK=false, IsPrFach=false     },  
                new HjFach { Name = "Religion"   , MinHalbjahre=2, Aufgabenfeld=(int)FachAufgabenfeld.Kein                 , IsLK=false, IsPrFach=false     },
                new HjFach { Name = "Astronomie" , MinHalbjahre=1, Aufgabenfeld=(int)FachAufgabenfeld.Naturwissenschaftlich, IsLK=false, IsPrFach=false     },
                new HjFach { Name = "Informatik" , MinHalbjahre=1, Aufgabenfeld=(int)FachAufgabenfeld.Naturwissenschaftlich, IsLK=false, IsPrFach=false     },
                new HjFach { Name = "Biologie"   , MinHalbjahre=1, Aufgabenfeld=(int)FachAufgabenfeld.Naturwissenschaftlich, IsLK=false, IsPrFach=false     },
                new HjFach { Name = "Philosophie", MinHalbjahre=1, Aufgabenfeld=(int)FachAufgabenfeld.Kein                 , IsLK=false, IsPrFach=false     },
                new HjFach { Name = "Geografie"  , MinHalbjahre=1, Aufgabenfeld=(int)FachAufgabenfeld.Gesellschaftlich     , IsLK=false, IsPrFach=false     },
                new HjFach { Name = "Sport"      , MinHalbjahre=1, Aufgabenfeld=(int)FachAufgabenfeld.Kein                 , IsLK=false, IsPrFach=false     },
                new HjFach { Name = "Musik"      , MinHalbjahre=2, Aufgabenfeld=(int)FachAufgabenfeld.Sprachlich           , IsLK=false, IsPrFach=false     },
                new HjFach { Name = "Kunst"      , MinHalbjahre=2, Aufgabenfeld=(int)FachAufgabenfeld.Sprachlich           , IsLK=false, IsPrFach=false     },
                new HjFach { Name = "Französich" , MinHalbjahre=4, Aufgabenfeld=(int)FachAufgabenfeld.Sprachlich           , IsLK=false, IsPrFach=false     },
                new HjFach { Name = "Geschichte" , MinHalbjahre=4, Aufgabenfeld=(int)FachAufgabenfeld.Gesellschaftlich     , IsLK=false, IsPrFach=false     },
                new HjFach { Name = "Spanisch"   , MinHalbjahre=4, Aufgabenfeld=(int)FachAufgabenfeld.Sprachlich           , IsLK=false, IsPrFach=false     }
            };


        }



    }
}
