using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotenApp.Models
{
    public class Halbjahr2Model
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Note1 { get; set; }
        public int? Note2 { get; set; }
        public int? Note3 { get; set; }
        public int? Note4 { get; set; }
        public int? Note5 { get; set; }
        public int? Note6 { get; set; }
        public int? Note7 { get; set; }
        public int? Note8 { get; set; }
        public int? Note9 { get; set; }
        public int? Note10 { get; set; }
        public int? Note11 { get; set; }
        public int? Note12 { get; set; }
        public int? KlausurNote1 { get; set; }
        public int? KlausurNote2 { get; set; }
        [Ignore]
        public List<int> LKNoten { get; set; }
        [Ignore]
        public List<int> KlausurNoten { get; set; }


        public Halbjahr2Model()
        {
            LKNoten = new List<int>();
            KlausurNoten = new List<int>();

        }
    }
}
