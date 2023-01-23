﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SQLite;

namespace NotenApp.Models
{
    public class Ziel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string FachName { get; set; }
        public int Halbjahr { get; set; }
        public string Beschreibung { get; set; }
        public int ErforderlicheNote { get; set; }
        public int ZielNote { get; set; }
    }
}