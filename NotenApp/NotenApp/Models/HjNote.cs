using System;
using System.Collections.Generic;
using System.Text;

namespace NotenApp.Models
{
    //Halbjahresnoten
    public class HjNote : BaseNote
    {
        public string Fach { get; set; }
        public int Halbjahr { get; set; }

    }
}
