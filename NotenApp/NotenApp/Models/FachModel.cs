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
        public float? Durchschnitt { get; set; }
        [Ignore]
        public List<NotenModel> LKNoten { get; set; }
        [Ignore]
        public List<NotenModel> KlausurNoten { get; set; }


        public FachModel()
        {
            LKNoten = new List<NotenModel>();
            KlausurNoten = new List<NotenModel>();
        }

        
    }
}
