using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotenApp.Models
{
    public class NotenModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Note { get; set; }
        public string Fach { get; set; }
        public int Halbjahr { get; set; }
        public int Type { get; set; }

    }
}
