using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotenApp.Models
{
    public class FachModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Halbjahr { get; set; }
        public float? LkDurchschnitt { get; set; }
        public float? KlausurDurchschnitt { get; set; }
        public float? GesamtDurchschnitt { get; set; }
        [Ignore]
        public List<int?> LKNoten { get; set; }
        [Ignore]
        public List<int?> KlausurNoten { get; set; }


        public FachModel()
        {
            LKNoten = new List<int?>();
            KlausurNoten = new List<int?>();
        }
    }
}
