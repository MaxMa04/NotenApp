using MvvmHelpers;
using MvvmHelpers.Commands;
using NotenApp.Logic;
using NotenApp.Models;
using NotenApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotenApp.ViewModels
{
    public class HalbjahrViewModel : INotifyPropertyChanged
    {
        private static HalbjahrViewModel instance = new HalbjahrViewModel();
        private float? gesamtDurchschnittHJ1;
        private float? gesamtDurchschnittHJ2;
        private float? gesamtDurchschnittHJ3;
        private float? gesamtDurchschnittHJ4;

        public static HalbjahrViewModel Instance { get { return instance; } }
        public ObservableRangeCollection<HjFach> FaecherHJ1 { get; set; }
        public ObservableRangeCollection<HjFach> FaecherHJ2 { get; set; }
        public ObservableRangeCollection<HjFach> FaecherHJ3 { get; set; }
        public ObservableRangeCollection<HjFach> FaecherHJ4 { get; set; }
        public AsyncCommand<HjFach> DeleteFachCommand { get; }
        public float? GesamtDurchschnittHJ1
        {
            get => gesamtDurchschnittHJ1;
            set
            {
                gesamtDurchschnittHJ1 = value;
                UserViewModel.Instance.GesamtDurchschnittHJ1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GesamtDurchschnittHJ1)));
            }
        }
        public float? GesamtDurchschnittHJ2
        {
            get => gesamtDurchschnittHJ2;
            set
            {
                gesamtDurchschnittHJ2 = value;
                UserViewModel.Instance.GesamtDurchschnittHJ2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GesamtDurchschnittHJ2)));
            }
        }
        public float? GesamtDurchschnittHJ3
        {
            get => gesamtDurchschnittHJ3;
            set
            {
                gesamtDurchschnittHJ3 = value;
                UserViewModel.Instance.GesamtDurchschnittHJ3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GesamtDurchschnittHJ3)));
            }
        }
        public float? GesamtDurchschnittHJ4
        {
            get => gesamtDurchschnittHJ4;
            set
            {
                gesamtDurchschnittHJ4 = value;
                UserViewModel.Instance.GesamtDurchschnittHJ4 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GesamtDurchschnittHJ4)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public HalbjahrViewModel()
        {
            FaecherHJ1 = new ObservableRangeCollection<HjFach>();
            FaecherHJ2 = new ObservableRangeCollection<HjFach>();
            FaecherHJ3 = new ObservableRangeCollection<HjFach>();
            FaecherHJ4 = new ObservableRangeCollection<HjFach>();
            DeleteFachCommand = new AsyncCommand<HjFach>(DeleteFachAsync);
        }
        public async Task DeleteFachAsync(HjFach fach)
        {
            var fach1 = FaecherHJ1.Where(f => f.Name == fach.Name).FirstOrDefault();
            var fach2 = FaecherHJ2.Where(f => f.Name == fach.Name).FirstOrDefault();
            var fach3 = FaecherHJ3.Where(f => f.Name == fach.Name).FirstOrDefault();
            var fach4 = FaecherHJ4.Where(f => f.Name == fach.Name).FirstOrDefault();
            FaecherHJ1.Remove(fach1);
            FaecherHJ2.Remove(fach2);
            FaecherHJ3.Remove(fach3);
            FaecherHJ4.Remove(fach4);
            await FachService.RemoveFach(fach); // Falls Fach Prüfungsfach ist wird es auch gelöscht!
            await Task.WhenAll(ChangeHjDurchschnitt(null), FachService.DeleteZiele(fach));
            await UserViewModel.Instance.InitZiele();
            
        }
        public void SolveDoubleFachHHJ1(HjFach fach)
        {
            if (FaecherHJ1.Where(f => f.Name == fach.Name).ToList().Count > 1)
            {
                FaecherHJ1.Remove(fach);
            }
        }
        public async Task UpdateFachState(HjFach fach)
        {

            var fach1 = FaecherHJ1.Where(f => f.Name == fach.Name).FirstOrDefault();
            var fach2 = FaecherHJ2.Where(f => f.Name == fach.Name).FirstOrDefault();
            var fach3 = FaecherHJ3.Where(f => f.Name == fach.Name).FirstOrDefault();
            var fach4 = FaecherHJ4.Where(f => f.Name == fach.Name).FirstOrDefault();
            FaecherHJ1.Remove(fach1);
            FaecherHJ2.Remove(fach2);
            FaecherHJ3.Remove(fach3);
            FaecherHJ4.Remove(fach4);
            List<HjFach> faecherToUpdate = await FachService.GetFaecherToUpdate(fach);
            await Task.WhenAll(GetNoten(faecherToUpdate[0]), GetNoten(faecherToUpdate[1]), GetNoten(faecherToUpdate[2]),GetNoten(faecherToUpdate[3]));
            FaecherHJ1.Add(faecherToUpdate[0]);
            FaecherHJ2.Add(faecherToUpdate[1]);
            FaecherHJ3.Add(faecherToUpdate[2]);
            FaecherHJ4.Add(faecherToUpdate[3]);
            FaecherHJ1 = Controller.SortList(FaecherHJ1);
            FaecherHJ2 = Controller.SortList(FaecherHJ2);
            FaecherHJ3 = Controller.SortList(FaecherHJ3);
            FaecherHJ4 = Controller.SortList(FaecherHJ4);

        }

        public async Task AddNote(HjFach fach, int note, NotenTyp notenTyp)
        {
            //await FachService.AddNote(fach, note, notenTyp);
            await Task.WhenAll(UpdateFachWhenNoteAdded(fach), ChangeHjDurchschnitt(fach.Halbjahr));
            await Task.WhenAll(FachService.UpdateUserB1(), UserViewModel.Instance.InitZiele());

        }
        public async Task ChangeHjDurchschnitt(int? halbjahr)
        {
            switch (halbjahr)
            {
                case 1:
                    GesamtDurchschnittHJ1 = await FachService.GetHJGesamtDurchschnitt(1);
                    break;
                case 2:

                    GesamtDurchschnittHJ2 = await FachService.GetHJGesamtDurchschnitt(2);

                    break;
                case 3:

                    GesamtDurchschnittHJ3 = await FachService.GetHJGesamtDurchschnitt(3);
                    break;
                case 4:

                    GesamtDurchschnittHJ4 = await FachService.GetHJGesamtDurchschnitt(4);
                    break;
                case null:
                    GesamtDurchschnittHJ1 = await FachService.GetHJGesamtDurchschnitt(1);
                    GesamtDurchschnittHJ2 = await FachService.GetHJGesamtDurchschnitt(2);
                    GesamtDurchschnittHJ3 = await FachService.GetHJGesamtDurchschnitt(3);
                    GesamtDurchschnittHJ4 = await FachService.GetHJGesamtDurchschnitt(4);
                    break;
            }

        }

        public async Task UpdateFachWhenNoteAdded(HjFach fach)
        {
            HjFach fachh = await FachService.GetFach(fach);
            await GetNoten(fachh);
            switch (fach.Halbjahr)
            {
                case 1:
                    FaecherHJ1.Remove(fach);
                    FaecherHJ1.Insert(0, fachh);
                    FaecherHJ1 = Controller.SortList(FaecherHJ1);
                    break;
                case 2:
                    FaecherHJ2.Remove(fach);
                    FaecherHJ2.Insert(0, fachh);
                    FaecherHJ2 = Controller.SortList(FaecherHJ2);
                    break;
                case 3:
                    FaecherHJ3.Remove(fach);
                    FaecherHJ3.Insert(0, fachh);
                    FaecherHJ3 = Controller.SortList(FaecherHJ3);
                    break;
                case 4:
                    FaecherHJ4.Remove(fach);
                    FaecherHJ4.Insert(0, fachh);
                    FaecherHJ4 = Controller.SortList(FaecherHJ4);
                    break;
            }
        }
        public async Task GetNoten(HjFach fach)
        {
            var lkn = await FachService.GetFachNotenHjView(fach, NotenTyp.LK);
            fach.LKNoten.AddRange(lkn);
            var kln = await FachService.GetFachNoten(fach, NotenTyp.Klausur);
            fach.KlausurNoten.AddRange(kln);

        }
        public async Task LoadInitialFaecher()
        {
            FaecherHJ1.Clear();
            var facher1 = await FachService.GetFaecher(1);

            foreach (var fach in facher1)
            {
                await GetNoten(fach);
            }
            facher1 = Controller.SortList(facher1);
            FaecherHJ1.AddRange(facher1);


            FaecherHJ2.Clear();
            var facher2 = await FachService.GetFaecher(2);
            foreach (var fach in facher2)
            {
                await GetNoten(fach);
            }
            facher2 = Controller.SortList(facher2);
            FaecherHJ2.AddRange(facher2);


            FaecherHJ3.Clear();
            var facher3 = await FachService.GetFaecher(3);
            foreach (var fach in facher3)
            {
                await GetNoten(fach);
            }
            facher3 = Controller.SortList(facher3);
            FaecherHJ3.AddRange(facher3);


            FaecherHJ4.Clear();
            var facher4 = await FachService.GetFaecher(4);


            foreach (var fach in facher4)
            {
                await GetNoten(fach);
            }
            facher4 = Controller.SortList(facher4);
            FaecherHJ4.AddRange(facher4);

        }
        public async Task<int?> GetPunktzahlBlock1()
        {
            await Task.WhenAll(FachService.EntscheideBioInfoPhysikChemie(), FachService.EntscheideGeoGRW(), FachService.EntscheideFremdsprache());

            List<HjFach> gesamtFaecher = await FachService.GetFaecher();


            List<HjFach> eingebrachteFaecher = new List<HjFach>(); //alle Fächer mit Halbjahren, die im Endeffekt eingebracht werden 
            List<HjFach> pflichtFaecher = new List<HjFach>(); //alle Fächer mit Halbjahren, die eingebracht werden müssen
            List<HjFach> uebrigeFaecher = new List<HjFach>(); //alle Fächer mit Halbjahren, die eingebracht werden könnten
            //Zuweisung der einzelnen FächerHalbjahre in Pflichtfächer und Übrige Fächer
            if (gesamtFaecher.Count < 40)
            {
                return null;
            }
            if (gesamtFaecher.Where(f => f.IsLK == true).ToList().Count != 8)
            {
                return null;
            }
            for (int j = 0; j < gesamtFaecher.Count; j++)
            {

                if (pflichtFaecher.Exists(t => t.Name == gesamtFaecher[j].Name) != true)
                {
                    List<HjFach> faecher = new List<HjFach>(); //Fächer mit gleichem Fachnamen aus verschiedenem Halbjahr

                    int anzahlDurchschnitte = 0;
                    float summeDurchschnitte = 0;
                    for (int i = 0; i < gesamtFaecher.Count; i++)
                    {
                        if (gesamtFaecher[j].Name == gesamtFaecher[i].Name)
                        {
                            faecher.Add(gesamtFaecher[i]);
                        }
                    }
                    foreach (var fachh in faecher)
                    {
                        if (fachh.Durchschnitt != null)
                        {
                            anzahlDurchschnitte++;
                            summeDurchschnitte += (float)fachh.Durchschnitt;
                        }
                    }
                    if (anzahlDurchschnitte == 0)
                    {
                        return null;
                    }
                    foreach (var fachhh in faecher)
                    {
                        if (fachhh.Durchschnitt == null)
                        {
                            fachhh.Durchschnitt = summeDurchschnitte / anzahlDurchschnitte;
                           
                        }
                    }

                    faecher = Controller.SortList(faecher);
                    switch (gesamtFaecher[j].EingebrachteHalbjahre)
                    {
                        case 1:
                            pflichtFaecher.Add(faecher[0]);
                            uebrigeFaecher.Add(faecher[1]);
                            uebrigeFaecher.Add(faecher[2]);
                            uebrigeFaecher.Add(faecher[3]);
                            break;
                        case 2:
                            pflichtFaecher.Add(faecher[0]);
                            pflichtFaecher.Add(faecher[1]);
                            uebrigeFaecher.Add(faecher[2]);
                            uebrigeFaecher.Add(faecher[3]);
                            break;
                        case 4:
                            pflichtFaecher.Add(faecher[0]);
                            pflichtFaecher.Add(faecher[1]);
                            pflichtFaecher.Add(faecher[2]);
                            pflichtFaecher.Add(faecher[3]);
                            break;

                    }
                    faecher.Clear();
                }
            }
            //Auffüllung der Eingebrachten Fächer Liste mit den besten übrigen Fächern
            eingebrachteFaecher.AddRange(pflichtFaecher);
            int nochaufzufuellen = 40 - eingebrachteFaecher.Count;
            uebrigeFaecher = Controller.SortList(uebrigeFaecher);
            for (int i = 0; i < nochaufzufuellen; i++)
            {
                eingebrachteFaecher.Add(uebrigeFaecher[i]);
            }
            //Eigentliche Berechnung der Punktzahl von Block 1
            double summeDurchschnitteAllerHalbjahre = 0;
            double punktzahlBlock1 = 0;
            foreach (var fach in eingebrachteFaecher)
            {
                if (fach.IsLK == true)
                {
                    summeDurchschnitteAllerHalbjahre += (int)Math.Round((float)fach.Durchschnitt, 0, MidpointRounding.AwayFromZero) * 2;
                }
                else
                {
                    summeDurchschnitteAllerHalbjahre += (int)Math.Round((float)fach.Durchschnitt, 0, MidpointRounding.AwayFromZero);
                }
            }
            punktzahlBlock1 = (summeDurchschnitteAllerHalbjahre / 48) * 40;
            if (punktzahlBlock1 < 0 || punktzahlBlock1 > 600)
            {
                throw new Exception("Punktzahl Block 1 funktioniert nicht haha viel Spaß du Looser! Halbjahrviewmodel Zeile 349");
            }
            else
            {
                return (int)Math.Round(punktzahlBlock1, 0, MidpointRounding.AwayFromZero);
            }

        }
    }
}
