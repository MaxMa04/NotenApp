using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotenApp.Models
{
    public class BaseNote
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Note { get; set; }
        public string Fach { get; set; }
        public int Typ { get; set; }
    }
}
