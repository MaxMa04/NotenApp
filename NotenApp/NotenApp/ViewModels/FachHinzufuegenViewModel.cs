using NotenApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotenApp.ViewModels
{
    public class FachHinzufuegenViewModel
    {
        public List<Halbjahr1Model> Faecher { get; set; }
        public FachHinzufuegenViewModel()
        {
            Faecher = new List<Halbjahr1Model>();

            Faecher.Add(new Halbjahr1Model { Name = "Mathe" });
            Faecher.Add(new Halbjahr1Model { Name = "Deutsch" });
            Faecher.Add(new Halbjahr1Model { Name = "Englisch" });
            Faecher.Add(new Halbjahr1Model { Name = "Informatik" });
            Faecher.Add(new Halbjahr1Model { Name = "Biologie" });
            Faecher.Add(new Halbjahr1Model { Name = "Geografie" });
            Faecher.Add(new Halbjahr1Model { Name = "Sport" });
            Faecher.Add(new Halbjahr1Model { Name = "Musik" });
            Faecher.Add(new Halbjahr1Model { Name = "Kunst" });
            Faecher.Add(new Halbjahr1Model { Name = "Französich" });
            Faecher.Add(new Halbjahr1Model { Name = "Geschichte" });
            Faecher.Add(new Halbjahr1Model { Name = "Spanisch" });


        }



    }
}
