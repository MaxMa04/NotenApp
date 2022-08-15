﻿using MvvmHelpers;
using MvvmHelpers.Commands;
using NotenApp.Models;
using NotenApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace NotenApp.ViewModels
{
    public class HalbjahrViewModel : INotifyPropertyChanged
    {
        public ObservableRangeCollection<FachModel> FaecherHJ1{ get; set; }
        public ObservableRangeCollection<FachModel> FaecherHJ2 { get; set; }
        public ObservableRangeCollection<FachModel> FaecherHJ3 { get; set; }
        public ObservableRangeCollection<FachModel> FaecherHJ4 { get; set; }
        public AsyncCommand<FachModel> RemoveCommand { get; }
        public AsyncCommand<int> RefreshCommand { get; }
        private float gesamtDurchschnittHJ1;
        public float GesamtDurchschnittHJ1
        {
            get => gesamtDurchschnittHJ1;
            set
            {                   
                gesamtDurchschnittHJ1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GesamtDurchschnittHJ1)));
            }
        }
        private float gesamtDurchschnittHJ2;
        public float GesamtDurchschnittHJ2
        {
            get => gesamtDurchschnittHJ2;
            set
            {
                gesamtDurchschnittHJ2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GesamtDurchschnittHJ2)));
            }
        }
        private float gesamtDurchschnittHJ3;
        public float GesamtDurchschnittHJ3
        {
            get => gesamtDurchschnittHJ3;
            set
            {
                gesamtDurchschnittHJ3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GesamtDurchschnittHJ3)));
            }
        }
        private float gesamtDurchschnittHJ4;
        public float GesamtDurchschnittHJ4
        {
            get => gesamtDurchschnittHJ4;
            set
            {
                gesamtDurchschnittHJ4 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GesamtDurchschnittHJ4)));
            }
        }
        public HalbjahrViewModel()
        {
            FaecherHJ1 = new ObservableRangeCollection<FachModel>();
            FaecherHJ2 = new ObservableRangeCollection<FachModel>();
            FaecherHJ3 = new ObservableRangeCollection<FachModel>();
            FaecherHJ4 = new ObservableRangeCollection<FachModel>();
            RefreshCommand = new AsyncCommand<int>(Refresh);
            RemoveCommand = new AsyncCommand<FachModel>(Remove);
            gesamtDurchschnittHJ1 = 10;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public async Task AddNote(FachModel fach, int note, int zahl)
        {
            await FachService.AddNote(fach, note, zahl);
            await Refresh(fach.Halbjahr);
            
        }
        public async Task Remove(FachModel fach)
        {
            await FachService.RemoveFach(fach);
            await Refresh(fach.Halbjahr);

        }

        public async Task Refresh(int halbjahr)
        {
            switch (halbjahr)
            {
                case 1:
                    FaecherHJ1.Clear();
                    var facher1 = await FachService.GetFacher(1);
                    var noten = await FachService.GetNoten(1);
                    FaecherHJ1.AddRange(facher1);
                    foreach (var fach in FaecherHJ1)
                    {
                        foreach (var note in noten)
                        {
                            if(note.Fach == fach.Name && note.Type == 1)
                            {
                                fach.LKNoten.Add(note);
                            }
                            if(note.Type == 2 && note.Fach == fach.Name)
                            {
                                fach.KlausurNoten.Add(note);
                            }
                        }
                    }

                    break;
                case 2:
                    FaecherHJ2.Clear();
                    var facher2 = await FachService.GetFacher(2);
                    var noten2 = await FachService.GetNoten(2);
                    FaecherHJ2.AddRange(facher2);
                    foreach (var fach in FaecherHJ2)
                    {
                        foreach (var note in noten2)
                        {
                            if (note.Fach == fach.Name && note.Type == 1)
                            {
                                fach.LKNoten.Add(note);
                            }
                            if (note.Type == 2 && note.Fach == fach.Name)
                            {
                                fach.KlausurNoten.Add(note);
                            }
                        }
                    }
                    
                    break;
                case 3:
                    FaecherHJ3.Clear();
                    var facher3 = await FachService.GetFacher(3);

                    FaecherHJ3.AddRange(facher3);
                    var noten3 = await FachService.GetNoten(3);
                    foreach (var fach in FaecherHJ3)
                    {
                        foreach (var note in noten3)
                        {
                            if (note.Fach == fach.Name && note.Type == 1)
                            {
                                fach.LKNoten.Add(note);
                            }
                            if (note.Type == 2 && note.Fach == fach.Name)
                            {
                                fach.KlausurNoten.Add(note);
                            }
                        }
                    }
                    break;
                case 4:
                    FaecherHJ4.Clear();
                    var facher4 = await FachService.GetFacher(4);

                    FaecherHJ4.AddRange(facher4);
                    var noten4 = await FachService.GetNoten(4);
                    foreach (var fach in FaecherHJ4)
                    {
                        foreach (var note in noten4)
                        {
                            if (note.Fach == fach.Name && note.Type == 1)
                            {
                                fach.LKNoten.Add(note);
                            }
                            if (note.Type == 2 && note.Fach == fach.Name)
                            {
                                fach.KlausurNoten.Add(note);
                            }
                        }
                    }
                    break;
            }
        }
    }
}
