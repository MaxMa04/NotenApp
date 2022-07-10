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
    public class HalbjahrViewModel
    {
        public ObservableRangeCollection<FachModel> FaecherHJ1{ get; set; }
        public ObservableRangeCollection<FachModel> FaecherHJ2 { get; set; }
        public ObservableRangeCollection<FachModel> FaecherHJ3 { get; set; }
        public ObservableRangeCollection<FachModel> FaecherHJ4 { get; set; }
        public AsyncCommand<FachModel> RemoveCommand { get; }
        public AsyncCommand<int> RefreshCommand { get; }
        public HalbjahrViewModel()
        {
            FaecherHJ1 = new ObservableRangeCollection<FachModel>();
            FaecherHJ2 = new ObservableRangeCollection<FachModel>();
            FaecherHJ3 = new ObservableRangeCollection<FachModel>();
            FaecherHJ4 = new ObservableRangeCollection<FachModel>();
            RefreshCommand = new AsyncCommand<int>(Refresh);
            RemoveCommand = new AsyncCommand<FachModel>(Remove);

        }
        public async Task AddNote(FachModel fach, int note, int zahl)
        {
            await FachService.AddNote2(fach, note, zahl);
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
                    FaecherHJ1.AddRange(facher1);
                    break;
                case 2:
                    FaecherHJ2.Clear();
                    var facher2 = await FachService.GetFacher(2);

                    FaecherHJ2.AddRange(facher2);
                    break;
                case 3:
                    FaecherHJ3.Clear();
                    var facher3 = await FachService.GetFacher(3);

                    FaecherHJ3.AddRange(facher3);
                    break;
                case 4:
                    FaecherHJ4.Clear();
                    var facher4 = await FachService.GetFacher(4);

                    FaecherHJ4.AddRange(facher4);
                    break;
            }
        }
    }
}
