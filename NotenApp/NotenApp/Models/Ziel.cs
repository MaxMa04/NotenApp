using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace NotenApp.Models
{
    public partial class Ziel : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int FachId { get; set; }
        public string FachName { get; set; }
        public int Halbjahr { get; set; }
        [ObservableProperty]
        int? erforderlicheLKNote;
        [ObservableProperty]
        int? erforderlicheKLNote;
        [ObservableProperty]
        int? zielNote;
    }
}
