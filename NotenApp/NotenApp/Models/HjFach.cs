﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotenApp.Models
{
    //Halbjahresfächer
    public class HjFach : BaseFach
    {
        public int Halbjahr { get; set; }
        [Ignore]
        public List<HjNote> LKNoten { get; set; }
        [Ignore]
        public List<HjNote> KlausurNoten { get; set; }


        public HjFach()
        {
            LKNoten = new List<HjNote>();
            KlausurNoten = new List<HjNote>();
        }
    }
}