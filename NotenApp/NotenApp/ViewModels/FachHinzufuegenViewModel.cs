using NotenApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotenApp.ViewModels
{
    public class FachHinzufuegenViewModel
    {
        public List<FachModel> Faecher { get; set; }
        public FachHinzufuegenViewModel()
        {
            Faecher = new List<FachModel>();

            Faecher.Add(new FachModel { Name = "Mathe" });
            Faecher.Add(new FachModel { Name = "Deutsch" });
            Faecher.Add(new FachModel { Name = "Englisch" });
            Faecher.Add(new FachModel { Name = "Informatik" });
            Faecher.Add(new FachModel { Name = "Biologie" });
            Faecher.Add(new FachModel { Name = "Geografie" });
            Faecher.Add(new FachModel { Name = "Sport" });
            Faecher.Add(new FachModel { Name = "Musik" });
            Faecher.Add(new FachModel { Name = "Kunst" });
            Faecher.Add(new FachModel { Name = "Französich" });
            Faecher.Add(new FachModel { Name = "Geschichte" });
            Faecher.Add(new FachModel { Name = "Spanisch" });


        }



    }
}
