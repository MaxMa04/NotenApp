using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotenApp.Models
{
    public class BaseFach
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public float? Durchschnitt { get; set; }
    }
}
