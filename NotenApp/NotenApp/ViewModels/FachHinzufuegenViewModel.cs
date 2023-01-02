using MvvmHelpers;
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
                new HjFach { Name = "Mathe"      , MinHalbjahre=4    },
                new HjFach { Name = "Deutsch"    , MinHalbjahre=4    },
                new HjFach { Name = "Englisch"   , MinHalbjahre=4    },
                new HjFach { Name = "Physik"     , MinHalbjahre=1    },
                new HjFach { Name = "G/R/W"      , MinHalbjahre=1    },
                new HjFach { Name = "Chemie"     , MinHalbjahre=1    },
                new HjFach { Name = "Ethik"      , MinHalbjahre=2    },
                new HjFach { Name = "Religion"   , MinHalbjahre=1    },
                new HjFach { Name = "Astronomie" , MinHalbjahre=1    },
                new HjFach { Name = "Informatik" , MinHalbjahre=1    },
                new HjFach { Name = "Biologie"   , MinHalbjahre=1    },
                new HjFach { Name = "Philosophie", MinHalbjahre=1    },
                new HjFach { Name = "Geografie"  , MinHalbjahre=1    },
                new HjFach { Name = "Sport"      , MinHalbjahre=1    },
                new HjFach { Name = "Musik"      , MinHalbjahre=2    },
                new HjFach { Name = "Kunst"      , MinHalbjahre=2    },
                new HjFach { Name = "Französich" , MinHalbjahre=4    },
                new HjFach { Name = "Geschichte", MinHalbjahre=4    },
                new HjFach { Name = "Spanisch"  , MinHalbjahre=4    }
            };


        }



    }
}
