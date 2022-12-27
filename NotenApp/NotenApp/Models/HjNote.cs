using System;
using System.Collections.Generic;
using System.Text;

namespace NotenApp.Models
{
    //Halbjahresnoten
    public class HjNote : BaseNote
    {
        public int Halbjahr { get; set; }
        public int Type { get; set; }

    }
}
