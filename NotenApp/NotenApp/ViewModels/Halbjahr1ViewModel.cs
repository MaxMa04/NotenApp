using MvvmHelpers;
using MvvmHelpers.Commands;
using NotenApp.Models;
using NotenApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotenApp.ViewModels
{
    public class Halbjahr1ViewModel
    {
        public ObservableRangeCollection<Halbjahr1Model> FaecherHJ1 { get; set; }
        public AsyncCommand<Halbjahr1Model> RemoveCommand { get; }
        public AsyncCommand RefreshCommand { get; }
        public Halbjahr1ViewModel()
        {
            FaecherHJ1 = new ObservableRangeCollection<Halbjahr1Model>();
            RefreshCommand = new AsyncCommand(Refresh);
            RemoveCommand = new AsyncCommand<Halbjahr1Model>(Remove);

        }
        public async Task AddNote(Halbjahr1Model fach, int note, int zahl)
        {
            await FachService.AddNote1(fach, note, zahl);
            await Refresh();
        }
        public async Task Remove(Halbjahr1Model fach)
        {
            await FachService.RemoveFach(fach.Id);
            await Refresh();
        }

        public async Task Refresh()
        {
            
            FaecherHJ1.Clear();
            var facher = await FachService.GetFaecherHJ1();

            FaecherHJ1.AddRange(facher);
        }


    }
}
