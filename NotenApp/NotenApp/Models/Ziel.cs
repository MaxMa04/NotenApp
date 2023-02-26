using System;
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
        public int FachId { get; set; }
        public string FachName { get; set; }
        public string Span1 { get; set; }
        public string Span2 { get; set; }
        public int Halbjahr { get; set; }
        public int ErforderlicheLKNote { get; set; }
        public int ErforderlicheKLNote { get; set; }
        public int? ZielNote { get; set; }
    }
}
