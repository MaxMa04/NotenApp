using CommunityToolkit.Mvvm.ComponentModel;
using NotenApp.Logic;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NotenApp.Models
{
    //Halbjahresnoten
    public partial class HJNote : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [ObservableProperty]
        private int note;

        public NotenTyp Typ { get; set; }
        public int FachId { get; set; }


    }
}
