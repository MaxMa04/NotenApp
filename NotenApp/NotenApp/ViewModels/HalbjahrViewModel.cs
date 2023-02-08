using MvvmHelpers;
using MvvmHelpers.Commands;
using NotenApp.Logic;
using NotenApp.Models;
using NotenApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NotenApp.ViewModels
{
    public class HalbjahrViewModel : INotifyPropertyChanged
    {
        public ObservableRangeCollection<HjFach> FaecherHJ1{ get; set; }
        public ObservableRangeCollection<HjFach> FaecherHJ2 { get; set; }
        public ObservableRangeCollection<HjFach> FaecherHJ3 { get; set; }
        public ObservableRangeCollection<HjFach> FaecherHJ4 { get; set; }
        public AsyncCommand<HjFach> RemoveCommand { get; }
        public AsyncCommand<int> RefreshCommand { get; }
        private float? bitte;
        public float? Bitte
        {
            get => bitte;
            set
            {
                bitte = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Bitte)));
            }
        }
        private float? gesamtDurchschnittHJ1;
        public float? GesamtDurchschnittHJ1
        {
            get => gesamtDurchschnittHJ1;
            set
            {                   
                gesamtDurchschnittHJ1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GesamtDurchschnittHJ1)));
            }
        }
        private float? gesamtDurchschnittHJ2;
        public float? GesamtDurchschnittHJ2
        {
            get => gesamtDurchschnittHJ2;
            set
            {
                gesamtDurchschnittHJ2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GesamtDurchschnittHJ2)));
            }
        }
        private float? gesamtDurchschnittHJ3;
        public float? GesamtDurchschnittHJ3
        {
            get => gesamtDurchschnittHJ3;
            set
            {
                gesamtDurchschnittHJ3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GesamtDurchschnittHJ3)));
            }
        }
        private float? gesamtDurchschnittHJ4;
        public float? GesamtDurchschnittHJ4
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
            FaecherHJ1 = new ObservableRangeCollection<HjFach>();
            FaecherHJ2 = new ObservableRangeCollection<HjFach>();
            FaecherHJ3 = new ObservableRangeCollection<HjFach>();
            FaecherHJ4 = new ObservableRangeCollection<HjFach>();
            RefreshCommand = new AsyncCommand<int>(Refresh);
            RemoveCommand = new AsyncCommand<HjFach>(Remove);
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public async Task AddNote(HjFach fach, int note, NotenTyp notenTyp)
        {
            await FachService.AddNote(fach, note, notenTyp);

            await AdjustFacher(fach);
            
            await Task.Run(async () => {
                await ChangeHjDurchschnitt(fach.Halbjahr);
            });


        }
        public async Task AdjustFacher(HjFach fach)
        {
            HjFach fachh = await FachService.GetFach(fach);
            await GetNoten(fachh);
            switch (fach.Halbjahr)
            {
                case 1:
                    await Task.Run(() => {
                        FaecherHJ1.Remove(fach);

                        FaecherHJ1.Insert(0, fachh);
                        FaecherHJ1 = Controller.SortList(FaecherHJ1);
                    }); 
                    break;
                case 2:

                    await Task.Run(() => {
                        FaecherHJ2.Remove(fach);
                                 
                        FaecherHJ2.Insert(0, fachh);
                        FaecherHJ2 = Controller.SortList(FaecherHJ2);
                    });

                    break;
                case 3:

                    await Task.Run(() => {
                        FaecherHJ3.Remove(fach);
                                 
                        FaecherHJ3.Insert(0, fachh);
                        FaecherHJ3 = Controller.SortList(FaecherHJ3);
                    });
                    break;
                case 4:

                    await Task.Run(() => {
                        FaecherHJ4.Remove(fach);
                                 
                        FaecherHJ4.Insert(0, fachh);
                        FaecherHJ4 = Controller.SortList(FaecherHJ4);
                    });
                    break;
            }
        }
        public async Task Remove(HjFach fach)
        {
            await FachService.RemoveFach(fach);
            await Task.Run( ()=> FaecherHJ1.Remove(fach));
            
            await ChangeHjDurchschnitt(fach.Halbjahr);
            
        }
        public async Task ChangeHjDurchschnitt(int halbjahr)
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
            }
        }
        public async Task GetNoten(HjFach fach)
        {
            await Task.Run (async () => { 
                var lkn = await FachService.GetFachNotenHjView(fach, NotenTyp.LK);
                fach.LKNoten.AddRange(lkn);
            });
            await Task.Run(async () => {
                var kln = await FachService.GetFachNoten(fach, NotenTyp.Klausur);

                fach.KlausurNoten.AddRange(kln);
            });
       
            
            
        }
        public async Task Refresh(int halbjahr)
        {
            switch (halbjahr)
            {
                case 1:
                    
                    FaecherHJ1.Clear();
                    var facher1 = await FachService.GetFaecher(1);

                    foreach (var fach in facher1)
                    {
                        await GetNoten(fach);
                    }
                    facher1 = Controller.SortList(facher1);
                    FaecherHJ1.AddRange(facher1);
                    await ChangeHjDurchschnitt(1);
                    break;
                case 2:
                    FaecherHJ2.Clear();
                    var facher2 = await FachService.GetFaecher(2);
                    foreach (var fach in facher2)
                    {
                        await GetNoten(fach);
                    }
                    facher2 = Controller.SortList(facher2);
                    FaecherHJ2.AddRange(facher2);
                    await ChangeHjDurchschnitt(2);

                    break;
                case 3:
                    FaecherHJ3.Clear();
                    var facher3 = await FachService.GetFaecher(3);
                    foreach (var fach in facher3)
                    {
                        await GetNoten(fach);
                    }
                    facher3 = Controller.SortList(facher3);
                    FaecherHJ3.AddRange(facher3);
                    await ChangeHjDurchschnitt(3);
                    break;
                case 4:
                    FaecherHJ4.Clear();
                    var facher4 = await FachService.GetFaecher(4);
                    

                    foreach (var fach in facher4)
                    {
                        await GetNoten(fach);
                    }
                    facher4 = Controller.SortList(facher4);
                    FaecherHJ4.AddRange(facher4);
                    await ChangeHjDurchschnitt(4);
                    break;
            }
        }

        public async Task<int?> GetPunktzahlBlock1()
        {
            await FachService.EntscheideBioInfoPhysikChemie();
            await FachService.EntscheideGeoGRW();
            await FachService.EntscheideFremdsprache();
            
            List<HjFach> gesamtFaecher = await FachService.GetFaecher();
            List<HjFach> eingebrachteFaecher = new List<HjFach>(); //alle Fächer mit Halbjahren, die im Endeffekt eingebracht werden 
            List<HjFach> pflichtFaecher = new List<HjFach>(); //alle Fächer mit Halbjahren, die eingebracht werden müssen
            List<HjFach> uebrigeFaecher = new List<HjFach>(); //alle Fächer mit Halbjahren, die eingebracht werden könnten
            //Zuweisung der einzelnen FächerHalbjahre in Pflichtfächer und Übrige Fächer
            if (gesamtFaecher.Count < 40 )
            {
                return 1;
            }
            if( gesamtFaecher.Exists(t => t.IsLK) == false)
            {
                return 2;
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
                    if(anzahlDurchschnitte == 0)
                    {
                        return 3;
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
                    summeDurchschnitteAllerHalbjahre += (int)Math.Round((float)fach.Durchschnitt,0) * 2;
                }
                else
                {
                    summeDurchschnitteAllerHalbjahre += (int)Math.Round((float)fach.Durchschnitt, 0);
                }
            }
            punktzahlBlock1 = (summeDurchschnitteAllerHalbjahre / 48) * 40;
            if(punktzahlBlock1 < 0 || punktzahlBlock1 > 600)
            {
                return 4;
            }
            else
            {
                return (int)Math.Round(punktzahlBlock1, 0);
            }
            
        }
    }
}
