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
        public ObservableRangeCollection<Fach> Facher { get; set; }
        public AsyncCommand<Fach> RemoveCommand { get; }
        public AsyncCommand RefreshCommand { get; }
        public Halbjahr1ViewModel()
        {
            Facher = new ObservableRangeCollection<Fach>();
            RefreshCommand = new AsyncCommand(Refresh);
            RemoveCommand = new AsyncCommand<Fach>(Remove);

        }
        public async Task AddNote(Fach fach, int note, int zahl)
        {
            await FachService.AddNote(fach, note, zahl);
            await Refresh();
        }
        public async Task Remove(Fach fach)
        {
            await FachService.RemoveFach(fach.Id);
            await Refresh();
        }

        public async Task Refresh()
        {
            
            Facher.Clear();
            var facher = await FachService.GetFacher();

            Facher.AddRange(facher);
        }


    }
}
