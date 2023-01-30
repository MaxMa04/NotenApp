using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotenApp.Models
{
    //Halbjahresnoten
    public class HJNote
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Note { get; set; }
        public int Typ { get; set; }
        public int FachId { get; set; }

    }
}
