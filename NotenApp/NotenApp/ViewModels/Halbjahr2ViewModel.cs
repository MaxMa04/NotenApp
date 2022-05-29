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
    public class Halbjahr2ViewModel
    {
        public ObservableRangeCollection<Halbjahr2Model> FaecherHJ2 { get; set; }
        public AsyncCommand<Halbjahr2Model> RemoveCommand { get; }
        public AsyncCommand RefreshCommand { get; }
        public Halbjahr2ViewModel()
        {
            FaecherHJ2 = new ObservableRangeCollection<Halbjahr2Model>();
            RefreshCommand = new AsyncCommand(Refresh);
            RemoveCommand = new AsyncCommand<Halbjahr2Model>(Remove);

        }
        public async Task AddNote(Halbjahr1Model fach, int note, int zahl)
        {
            await FachService.AddNote(fach, note, zahl);
            await Refresh();
        }
        public async Task Remove(Halbjahr2Model fach)
        {
            await FachService.RemoveFach(fach.Id);
            await Refresh();
        }

        public async Task Refresh()
        {

            FaecherHJ2.Clear();
            var facher = await FachService.GetFaecherHJ2();

            FaecherHJ2.AddRange(facher);
        }
    }
}
