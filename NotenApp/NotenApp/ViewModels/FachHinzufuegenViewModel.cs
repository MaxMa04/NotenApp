using NotenApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotenApp.ViewModels
{
    public class FachHinzufuegenViewModel
    {
        public List<Fach> Faecher { get; set; }
        public FachHinzufuegenViewModel()
        {
            Faecher = new List<Fach>();

            Faecher.Add(new Fach { Name = "Mathe" });
            Faecher.Add(new Fach { Name = "Deutsch" });
            Faecher.Add(new Fach { Name = "Englisch" });
            Faecher.Add(new Fach { Name = "Informatik" });
            Faecher.Add(new Fach { Name = "Biologie" });
            Faecher.Add(new Fach { Name = "Geografie" });
            Faecher.Add(new Fach { Name = "Sport" });
            Faecher.Add(new Fach { Name = "Musik" });
            Faecher.Add(new Fach { Name = "Kunst" });
            Faecher.Add(new Fach { Name = "Französich" });
            Faecher.Add(new Fach { Name = "Geschichte" });
            Faecher.Add(new Fach { Name = "Spanisch" });


        }



    }
}
