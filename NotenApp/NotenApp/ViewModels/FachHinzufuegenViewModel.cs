using NotenApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotenApp.ViewModels
{
    public class FachHinzufuegenViewModel
    {
        public List<HjFach> Faecher { get; set; }
        public FachHinzufuegenViewModel()
        {
            Faecher = new List<HjFach>();

            Faecher.Add(new HjFach { Name = "Mathe" });
            Faecher.Add(new HjFach { Name = "Deutsch" });
            Faecher.Add(new HjFach { Name = "Englisch" });
            Faecher.Add(new HjFach { Name = "Informatik" });
            Faecher.Add(new HjFach { Name = "Biologie" });
            Faecher.Add(new HjFach { Name = "Geografie" });
            Faecher.Add(new HjFach { Name = "Sport" });
            Faecher.Add(new HjFach { Name = "Musik" });
            Faecher.Add(new HjFach { Name = "Kunst" });
            Faecher.Add(new HjFach { Name = "Französich" });
            Faecher.Add(new HjFach { Name = "Geschichte" });
            Faecher.Add(new HjFach { Name = "Spanisch" });


        }



    }
}
