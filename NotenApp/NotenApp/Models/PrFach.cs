using System;
using System.Collections.Generic;
using System.Text;

namespace NotenApp.Models
{
    //Prüfungsfächer
    public class PrFach : BaseFach
    {
        public int PrNummer { get; set; }
        public int? NoteMündlich { get; set; }
        public int? NachNoteMündlich { get; set; }
        public int? NoteSchriftlich { get; set; }
    }
}
