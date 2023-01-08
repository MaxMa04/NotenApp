using NotenApp.Logic;
using NotenApp.Models;
using NotenApp.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NotenApp.Services
{
    public static class FachService
    {

        static SQLiteAsyncConnection db;
        static async Task Init()
        {
            //if(db!= null)
            //{
            //    return;
            //}
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Data");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<HjFach>();
            await db.CreateTableAsync<PrFach>();
            await db.CreateTableAsync<HjNote>();
            await db.CreateTableAsync<PrNote>();
        }

        //Halbjahre
        public static async Task AddNote(HjFach fach, int note, NotenTyp notenTyp)
        {
            await Init();

            HjNote newNote = new HjNote
            {
                Note = note,
                Typ = (int)notenTyp,
                Fach = fach.Name,
                Halbjahr = fach.Halbjahr
            };
            await db.InsertAsync(newNote);
            fach.Durchschnitt = await GetFachDurchschnitt(fach);
            await db.UpdateAsync(fach);
            if(fach.Name == "Informatik" || fach.Name == "Biologie" || fach.Name=="Physik"|| fach.Name == "Chemie")
            {
                await EntscheideBioInfoPhysikChemie();
            }


        }

        public static async Task<List<HjFach>> GetFaecher(int halbjahr)
        {
            await Init();
            var Gesamtfacher = await db.Table<HjFach>().ToListAsync();
            List<HjFach> facher = new List<HjFach>();
            foreach (var fach in Gesamtfacher)
            {
                if(fach.Halbjahr == halbjahr)
                {
                    facher.Add(fach);
                }
            }
            return facher;
        }
        public static async Task<int> GetEinzubringendeHalbjahre(HjFach fach)
        {
            await Init();
            if (fach.Name == "Biologie" || fach.Name == "Chemie" || fach.Name == "Physik" || fach.Name == "Informatik")
            {
                await EntscheideBioInfoPhysikChemie();
            }
            if (fach.Name == "G/R/W" || fach.Name == "Geografie")
            {
                await EntscheideGeoGRW();
            }
            if(fach.IsFremdsprache == true)
            {
                await EntscheideFremdsprache();
            }

            List<HjFach> facher = await GetFaecher(fach.Halbjahr);
            foreach (var item in facher)
            {
                if(item.Name == fach.Name)
                {
                    return item.EingebrachteHalbjahre;
                    
                }
            }
            return -500;
            
        }
        public static async Task<List<HjFach>> GetFaecher()
        {
            await Init();
            List<HjFach> Gesamtfacher = await db.Table<HjFach>().ToListAsync();
            return Gesamtfacher;
        }
        public static async Task<List<HjNote>> GetNoten(int halbjahr)
        {
            await Init();
            var noten = await db.Table<HjNote>().ToListAsync();
            List<HjNote> Noten = new List<HjNote>();
            foreach (var note in noten)
            {
                if (note.Halbjahr == halbjahr)
                {
                    Noten.Add(note);
                }
            }
            return Noten;
        }
        public static async Task AddFach(string name, int aufgabenfeld, int halbjahr, int minHalbjahre, bool isLK, bool isPrFach, bool isFremdsprache) 
        {
            await Init();

            HjFach fach = new HjFach
            {
                Name = name,
                Aufgabenfeld = aufgabenfeld,
                Halbjahr = halbjahr,
                MinHalbjahre = minHalbjahre,
                EingebrachteHalbjahre = minHalbjahre,
                IsLK = isLK,
                IsPrFach= isPrFach,
                IsFremdsprache = isFremdsprache
                
            };

            await db.InsertAsync(fach);
        }
        public static async Task RemoveSingleNote(HjNote note)
        {
            await Init();
            
            await db.DeleteAsync<HjNote>(note.Id);
            var Gesamtfacher = await db.Table<HjFach>().ToListAsync();
            foreach (var fach in Gesamtfacher)
            {
                if(fach.Halbjahr == note.Halbjahr && fach.Name == note.Fach)
                {
                    fach.Durchschnitt = await GetFachDurchschnitt(fach);
                    await db.UpdateAsync(fach);
                }
            }
        }
        public static async Task RemoveFach(HjFach fach)
        {
            await Init();
            List<HjFach> list = await db.Table<HjFach>().ToListAsync();
            foreach (var item in list)
            {
                if(item.Name == fach.Name)
                {
                    await db.DeleteAsync<HjFach>(item.Id);
                }
            }
            await RemoveAllNoten(fach);
            await db.DeleteAsync<HjFach>(fach.Id);
        }
        public static async Task RemoveAllNoten(HjFach fach)
        {
            await Init();
            List<HjNote> noten = await db.Table<HjNote>().ToListAsync();
            foreach (var note in noten)
            {
                if (note.Fach == fach.Name)
                {
                    await db.DeleteAsync<HjNote>(note.Id);
                }
            }
        }
        public static async Task<float?> GetFachDurchschnitt(HjFach fach)
        {
            float? durchschnitt;
            float? durchschnittLk;
            float? countLk = 0;
            float? durchschnittKlausur;
            float? countKlausur = 0;
            List<float?> LKNoten = new List<float?>();
            List<float?> KlausurNoten = new List<float?>();
            bool hasLk = false;
            bool hasKlausur = false;
            List<HjNote> gesamtNoten = await db.Table<HjNote>().ToListAsync();
            foreach (var item in gesamtNoten)
            {
                switch (item.Typ)
                {
                    case 1:
                        if(item.Fach == fach.Name && item.Halbjahr == fach.Halbjahr)
                        {
                            LKNoten.Add(item.Note);
                        }
                        break;
                    case 2:
                        if (item.Fach == fach.Name && item.Halbjahr == fach.Halbjahr)
                        {
                            KlausurNoten.Add(item.Note);
                        }
                        break;
                }
   
            }
            for (int i = 0; i < LKNoten.Count; i++)
            {
                if (LKNoten[i] != null)
                {
                    countLk += LKNoten[i];
                    hasLk = true;
                }
            }
            durchschnittLk = countLk / LKNoten.Count;
            for (int i = 0; i < KlausurNoten.Count; i++)
            {
                if (KlausurNoten[i] != null)
                {
                    countKlausur += KlausurNoten[i];
                    hasKlausur = true;
                }
            }
            durchschnittKlausur = countKlausur / KlausurNoten.Count;
            
            if(hasKlausur == false && hasLk == false)
            {
                return null;
            }
            if(hasKlausur == false)
            {
                return (float?)Math.Round((decimal)durchschnittLk,1);
            }
            else if(hasLk == false)
            {
                return durchschnittKlausur;
            }
            else
            {
                var y = durchschnittLk + durchschnittKlausur;
                durchschnitt = y / 2;
                return (float?)Math.Round((decimal)durchschnitt,1);
            }
        }
        public static async Task<float?> GetHJGesamtDurchschnitt(int halbjahr)
        {
            await Init();
            var Gesamtfacher = await db.Table<HjFach>().ToListAsync();
            float gesamtDurchschnitt;
            float count = 0;
            float count2 = 0;
                            
            foreach (var item in Gesamtfacher)
            {
                if(item.Halbjahr == halbjahr && item.Durchschnitt != null)
                {
                    count += (float)item.Durchschnitt;
                    count2++;
                }
                
            }
            if(count2 == 0)
            {
                return null;
            }
            else
            {
                gesamtDurchschnitt = count / count2;
                return (float)Math.Round(gesamtDurchschnitt, 1);
            }
        }
        //Prüfungen/Block2
        public static async Task AddPrFach(string name, int prNummer)
        {
            await Init();
            List<HjFach> HjFaecher = await db.Table<HjFach>().ToListAsync();
            foreach (var item in HjFaecher)
            {
                if (item.Name == name)
                {
                    if(prNummer == 1 || prNummer == 2)
                    {
                        item.EingebrachteHalbjahre = 4;
                        item.IsPrFach = true;
                        item.IsLK = true;
                        await db.UpdateAsync(item);
                    }
                    else
                    {
                        item.EingebrachteHalbjahre = 4;
                        item.IsPrFach = true;
                        await db.UpdateAsync(item);
                    }
                    
                }
            }
            PrFach fach = new PrFach()
            {
                Name = name,
                PrNummer = prNummer
            };
        
            await db.InsertAsync(fach);
        }
        public static async Task AddPrNote(int prNummer, int note)
        {
            await Init();
        
            PrNote prnote = new PrNote()
            {
                Note = note,
                PrNummer = prNummer
            };
        
            await db.InsertAsync(prnote);
        }
        public static async Task UpdateName(string name, int prNummer)
        {
            await Init();
            List<PrFach> PrFaecher = await db.Table<PrFach>().ToListAsync();
            List<HjFach> HjFaecher = await db.Table<HjFach>().ToListAsync();
            foreach (var hjFach in HjFaecher) //einzubringende Halbjahre werden angepasst für altes Fach
            {
                foreach (var fach in PrFaecher)
                {
                    
                    if(hjFach.Name == fach.Name && fach.PrNummer == prNummer)
                    {
                        hjFach.EingebrachteHalbjahre = hjFach.MinHalbjahre;
                        hjFach.IsPrFach = false;
                        hjFach.IsLK = false;
                        await db.UpdateAsync(hjFach);

                    }
                }
            }
            foreach (var item in PrFaecher) //neuer Name wird zugewiesen + einzubringende Halbjahre werden angepasst für neues Fach
            {

                if(item.PrNummer == prNummer)
                {
                    item.Name = name;
                    await db.UpdateAsync(item);
                    foreach (var hjFach in HjFaecher)
                    {
                        if(hjFach.Name == name)
                        {
                            if(prNummer == 1 || prNummer == 2)
                            {
                                hjFach.EingebrachteHalbjahre = 4;
                                hjFach.IsPrFach = true;
                                hjFach.IsLK=true;

                                await db.UpdateAsync(hjFach);
                            }
                            else
                            {
                                hjFach.EingebrachteHalbjahre = 4;
                                hjFach.IsPrFach = true;
                                await db.UpdateAsync(hjFach);
                            }
                            
                        } 
                    }
                    
                }
            }
        }
        public static async Task UpdateNote(int? note, int prNummer, NotenTyp notenTyp)
        {
            await Init();
            List<PrFach> PrFaecher = await db.Table<PrFach>().ToListAsync();
            foreach (var item in PrFaecher)
            {
                if (item.PrNummer == prNummer)
                {
                    switch (notenTyp)
                    {
                        case NotenTyp.Schriftlich:
                            item.NoteSchriftlich = note;
                            break;
                        case NotenTyp.Mündlich:
                            item.NoteMündlich = note;
                            break;
                    }
                    if(item.NoteSchriftlich != null && item.NoteMündlich != null)
                    {
                        item.Durchschnitt = (item.NoteSchriftlich + item.NoteMündlich) / 2;
                    }
                    else if(item.NoteSchriftlich == null && item.NoteMündlich == null)
                    {
                        item.Durchschnitt = null;
                    }
                    else if(item.NoteSchriftlich == null && item.NoteMündlich != null)
                    {
                        item.Durchschnitt = item.NoteMündlich;
                    }
                    else
                    {
                        item.Durchschnitt = item.NoteSchriftlich;
                    }
                    await db.UpdateAsync(item);
                    
                }
            }

        }
        public static async Task<PrFach> GetPrFach(int prNummer)
        {
            await Init();
            List<PrFach> PrFaecher = await db.Table<PrFach>().ToListAsync();
            PrFach prfach = new PrFach();
            foreach (var fach in PrFaecher)
            {
                if (fach.PrNummer == prNummer)
                {
                    prfach = fach;
                }
            }
            if(prfach.Name == null)
            {
                return null;
            }
            else
            {
                return prfach;
            }
        }
        public static async Task<float?> GetDurchschnittBlock2()
        {
            await Init();
            List<PrFach> prFaecher = await db.Table<PrFach>().ToListAsync();
            int anzahlFaecher = 0;
            float? durchschnittsSumme = 0;
            float? durchschnittBlock2;
            foreach (var fach in prFaecher)
            {
                if(fach.Durchschnitt != null)
                {
                    anzahlFaecher++;
                    durchschnittsSumme += fach.Durchschnitt;
                }
            }
            if(anzahlFaecher == 0)
            {
                return null;
            }
            durchschnittBlock2 = durchschnittsSumme / anzahlFaecher;
            return durchschnittBlock2;
        }
        public static async Task<string> GetPunktzahlBlock2()
        {
            await Init();
            float? durchschnitt = await GetDurchschnittBlock2();
            float? iPunktzahl =  durchschnitt * 20;
            if(iPunktzahl > 0)
            {
                string punktzahl = (int?)iPunktzahl + "/300";

                return punktzahl;
            }
            else
            {
                return "-";
            }

        }
        public static async Task Delete()
        {
            await Init();
            await db.DeleteAllAsync<PrFach>();
            await db.DeleteAllAsync<PrNote>();
        }
        //Abitur
        public static async Task<float?> GetAbiturNote()
        {
            await Init();
            int abipunktzahl = (int)await GetAbiturPunktzahl();
            List<int> punktzahlen = new List<int> { 300,301,319,337,355,373,391,409,427,445,463,481,499,517,535,553,571,
                589,607,625,643,661,679,697,715,733,751,769,787,805,823};
            int stelle = 0;
            float abiturNote;
            if (abipunktzahl < 4)
            {
                return -1;
            }
            for (int i = 0; i < punktzahlen.Count; i++)
            {
                if(abipunktzahl >= punktzahlen[i])
                {
                    stelle = i;
                }
                else
                {
                    break;
                }
            }

            abiturNote = 4.0f - 0.1f * stelle;

            return abiturNote;
        }
        
        
        public static async Task<float> GetAbiturPunktzahl()
        {
            await Init();
            HalbjahrViewModel model = new HalbjahrViewModel();
            float? durchschnittBlock2 = await GetDurchschnittBlock2();
            float punktzahlBlock1 = (int)await model.GetPunktzahlBlock1();
            
            
            if(durchschnittBlock2 != null && punktzahlBlock1 > 3)
            {
                float punktzahlBlock2 = (float)Math.Round((double)durchschnittBlock2, 0) * 20;
                float abipunktzahl = punktzahlBlock2 + punktzahlBlock1;
                return abipunktzahl;
            }
            else if(durchschnittBlock2== null && punktzahlBlock1 > 3)
            {
                float schBlock2 = punktzahlBlock1 / 600 * 300;
                float abipunktzahl = (float)Math.Round((double)schBlock2,0) + punktzahlBlock1;
                return abipunktzahl;
            }
            else
            {
                return punktzahlBlock1;
            }
 
        }
        public static async Task EntscheideFremdsprache()
        {
            await Init();
            List<HjFach> FaecherHj = await GetFaecher(1);
            List<HjFach> GesamtFaecher = await GetFaecher();
            int anzFS = 0;
            string NaFS1 = null;
            double suFs1 = 0;
            double anFs1 = 0;
            string NaFS2 = null;
            double suFs2 = 0;
            double anFs2 = 0;
            string NaFS3 = null;
            double suFs3 = 0;
            double anFs3 = 0;
            double duFs1;
            double duFs2;
            double duFs3;
            foreach (var item in FaecherHj)
            {
                if (item.IsFremdsprache == true)
                {
                    anzFS++;
                    if (NaFS1 == null)
                    {
                        NaFS1 = item.Name;
                        foreach (var fach in GesamtFaecher)
                        {
                            if (fach.Name == NaFS1 && fach.Durchschnitt != null)
                            {
                                suFs1 += (double)fach.Durchschnitt;
                                anFs1++;
                            }
                            if (fach.Name == NaFS1 && fach.IsPrFach == true)
                            {
                                return;
                            }
                            else
                            {
                                fach.EingebrachteHalbjahre = fach.MinHalbjahre;
                                await db.UpdateAsync(fach);
                            }
                        }
                    }
                    else if (NaFS2 == null)
                    {
                        NaFS2 = item.Name;
                        foreach (var fach in GesamtFaecher)
                        {
                            if (fach.Name == NaFS2 && fach.Durchschnitt != null)
                            {
                                suFs2 += (double)fach.Durchschnitt;
                                anFs2++;
                            }
                            if (fach.Name == NaFS2 && fach.IsPrFach == true)
                            {
                                return;
                            }
                            else
                            {
                                fach.EingebrachteHalbjahre = fach.MinHalbjahre;
                                await db.UpdateAsync(fach);
                            }
                        }
                    }
                    else
                    {
                        NaFS3 = item.Name;
                        foreach (var fach in GesamtFaecher)
                        {
                            if (fach.Name == NaFS3 && fach.Durchschnitt != null)
                            {
                                suFs3 += (double)fach.Durchschnitt;
                                anFs3++;
                            }
                            if (fach.Name == NaFS3 && fach.IsPrFach == true)
                            {
                                return;
                            }
                            else
                            {
                                fach.EingebrachteHalbjahre = fach.MinHalbjahre;
                                await db.UpdateAsync(fach);
                            }
                        }
                    }
                }


            }
            switch (anzFS)
            {
                case 1:
                    foreach (var item in GesamtFaecher)
                    {
                        if(item.IsFremdsprache == true)
                        {
                            item.EingebrachteHalbjahre = 4;
                            await db.UpdateAsync(item);
                        }
                    }
                    break;
                case 2:
                    duFs1 = suFs1 / anFs1;
                    duFs2 = suFs2 / anFs2;
                    List<double> durchschnitted = new List<double> { duFs1, duFs2};
                    durchschnitted.Sort();
                    if (duFs1 == durchschnitted[1])
                    {
                        foreach (var item in GesamtFaecher)
                        {
                            if (item.Name == NaFS1)
                            {
                                item.EingebrachteHalbjahre = 4;
                                await db.UpdateAsync(item);
                            }
                        }
                    }
                    else
                    {
                        foreach (var item in GesamtFaecher)
                        {
                            if (item.Name == NaFS2)
                            {
                                item.EingebrachteHalbjahre = 4;
                                await db.UpdateAsync(item);
                            }
                        }
                    }

                    break;
                case 3:
                    duFs1 = suFs1 / anFs1;
                    duFs2 = suFs2 / anFs2;
                    duFs3 = suFs3 / anFs3;
                    List<double> durchschnitte = new List<double> { duFs1, duFs2, duFs3 };
                    durchschnitte.Sort();
                    if (duFs1 == durchschnitte[2])
                    {
                        foreach (var item in GesamtFaecher)
                        {
                            if (item.Name == NaFS1)
                            {
                                item.EingebrachteHalbjahre = 4;
                                await db.UpdateAsync(item);
                            }
                        }
                    }
                    else if (duFs2 == durchschnitte[2])
                    {
                        foreach (var item in GesamtFaecher)
                        {
                            if (item.Name == NaFS2)
                            {
                                item.EingebrachteHalbjahre = 4;
                                await db.UpdateAsync(item);
                            }
                        }
                    }
                    else
                    {
                        foreach (var item in GesamtFaecher)
                        {
                            if (item.Name == NaFS3)
                            {
                                item.EingebrachteHalbjahre = 4;
                                await db.UpdateAsync(item);
                            }
                        }
                    }
                    break;
                default:
                    break;
            }


        }

        public static async Task EntscheideGeoGRW()
        {
            await Init();
            List<HjFach> Faecher = await GetFaecher();
            if(Faecher.Exists(t => t.Name == "G/R/W") == false)
            {
                foreach (var item in Faecher)
                {
                    if(item.Name == "Geografie" && item.IsPrFach != true)
                    {
                        item.EingebrachteHalbjahre = 2;
                        await db.UpdateAsync(item);
                    }
                }
                return;
            }
            if (Faecher.Exists(t => t.Name == "Geografie") == false)
            {
                foreach (var item in Faecher)
                {
                    if (item.Name == "G/R/W" && item.IsPrFach != true)
                    {
                        item.EingebrachteHalbjahre = 2;
                        await db.UpdateAsync(item);
                    }
                }
                return;
            }
            if(Faecher.Exists(t => t.Name == "G/R/W") == true && Faecher.Exists(t => t.Name == "Geografie") == true)
            {
                double sumGeo = 0;
                double anzGeo = 0;
                double sumGRW = 0;
                double anzGRW = 0;
                foreach (var item in Faecher)
                {
                    switch (item.Name)
                    {
                        case "G/R/W":
                            if (item.Durchschnitt != null)
                            {
                                sumGRW += (double)item.Durchschnitt;
                                anzGRW++;
                            }
                            if (item.IsPrFach == true)
                            {
                                return;
                            }
                            else
                            {
                                item.EingebrachteHalbjahre = item.MinHalbjahre;
                                await db.UpdateAsync(item);
                            }
                            break;
                        case "Geografie":
                            if (item.Durchschnitt != null)
                            {
                                sumGeo += (double)item.Durchschnitt;
                                anzGeo++;
                            }
                            if (item.IsPrFach == true)
                            {
                                return;
                            }
                            else
                            {
                                item.EingebrachteHalbjahre = item.MinHalbjahre;
                                await db.UpdateAsync(item);
                            }
                            break;
                        default:
                            break;
                    }
                }
                double duGRW = sumGRW / anzGRW;
                double duGeo = sumGeo / anzGeo;
                if(duGRW > duGeo)
                {
                    foreach (var item in Faecher)
                    {
                        if(item.Name == "G/R/W")
                        {
                            item.EingebrachteHalbjahre = 2;
                            await db.UpdateAsync(item);
                        }
                    }
                }
                else
                {
                    foreach (var item in Faecher)
                    {
                        if (item.Name == "Geografie")
                        {
                            item.EingebrachteHalbjahre = 2;
                            await db.UpdateAsync(item);
                        }
                    }
                }
            }

        }
        public static async Task EntscheideBioInfoPhysikChemie()
        {
            await Init();
            List<HjFach> Faecher = await GetFaecher();
            double sumCh = 0;
            double anzCh = 0;
            double sumPh = 0;
            double anzPh = 0;
            double sumIn = 0;
            double anzIn = 0;
            double sumBi = 0;
            double anzBi = 0;
            int anzprfaecher = 0;
            foreach (var item in Faecher)
            {
                switch (item.Name)
                {
                    case "Chemie":
                        if(item.Durchschnitt != null)
                        {
                            sumCh += (double)item.Durchschnitt;
                            anzCh++;
                        }
                        if (item.IsPrFach == true)
                        {
                            sumCh = 0;
                            anzprfaecher++;
                        }
                        else
                        {
                            item.EingebrachteHalbjahre = item.MinHalbjahre;
                            await db.UpdateAsync(item);
                        }
                        break;
                    case "Biologie":
                        if (item.Durchschnitt != null)
                        {
                            sumBi += (double)item.Durchschnitt;
                            anzBi++;
                        }
                        if (item.IsPrFach == true)
                        {
                            sumBi = 0; 
                            anzprfaecher++;
                        }
                        else
                        {
                            item.EingebrachteHalbjahre = item.MinHalbjahre;
                            await db.UpdateAsync(item);
                        }
                        break;
                    case "Physik":
                        if (item.Durchschnitt != null)
                        {
                            sumPh += (double)item.Durchschnitt;
                            anzPh++;
                        }
                        if (item.IsPrFach == true)
                        {
                            sumPh = 0;
                            anzprfaecher++;
                        }
                        else
                        {
                            item.EingebrachteHalbjahre = item.MinHalbjahre;
                            await db.UpdateAsync(item);
                        }
                        break;
                    case "Informatik":
                        if (item.Durchschnitt != null)
                        {
                            sumIn += (double)item.Durchschnitt;
                            anzIn++;
                        }
                        if (item.IsPrFach == true)
                        {
                            sumIn = 0;
                            anzprfaecher++;
                        }
                        else
                        {
                            item.EingebrachteHalbjahre = item.MinHalbjahre;
                            await db.UpdateAsync(item);
                        }
                        break;
                    default:
                        break;
                }

            }
            if(anzprfaecher >= 8)
            {
                return;
            }
            if(anzprfaecher == 4)
            {
                double duIn = sumIn / anzIn;
                double duPh = sumPh / anzPh;
                double duCh = sumCh / anzCh;
                double duBi = sumBi / anzBi;
                int fach1 = 0;
                List<double> numbers = new List<double> { duIn, duPh, duCh, duBi };
                // Liste sortieren
                List<double> numbersa = numbers.ToList();
                numbers.Sort();
                for (int i = 0; i < numbers.Count; i++)
                {
                    if (numbers[3] == numbersa[i])
                    {
                        fach1 = i;
                    }
                }
                switch (fach1)
                {
                    case 0:
                        foreach (var item in Faecher)
                        {
                            if (item.Name == "Informatik")
                            {
                                item.EingebrachteHalbjahre = 4;
                                await db.UpdateAsync(item);
                            }
                        }
                        break;
                    case 1:
                        foreach (var item in Faecher)
                        {
                            if (item.Name == "Physik")
                            {
                                item.EingebrachteHalbjahre = 4;
                                await db.UpdateAsync(item);
                            }
                        }
                        break;
                    case 2:
                        foreach (var item in Faecher)
                        {
                            if (item.Name == "Chemie")
                            {
                                item.EingebrachteHalbjahre = 4;
                                await db.UpdateAsync(item);
                            }
                        }
                        break;
                    case 3:
                        foreach (var item in Faecher)
                        {
                            if (item.Name == "Biologie")
                            {
                                item.EingebrachteHalbjahre = 4;
                                await db.UpdateAsync(item);
                            }
                        }
                        break;
                }
            }
            else
            {
                double duIn = sumIn / anzIn;
                double duPh = sumPh / anzPh;
                double duCh = sumCh / anzCh;
                double duBi = sumBi / anzBi;
                int fach1 = 0;
                int fach2 = 0;
                List<double> numbers = new List<double> { duIn, duPh, duCh, duBi };
                // Liste sortieren
                List<double> numbersa = numbers.ToList();
                numbers.Sort();
                for (int i = 0; i < numbers.Count; i++)
                {
                    if (numbers[3] == numbersa[i])
                    {
                        fach1 = i;
                    }
                    if (numbers[2] == numbersa[i])
                    {
                        fach2 = i;
                    }
                }
                switch (fach2)
                {
                    case 0:
                        foreach (var item in Faecher)
                        {
                            if (item.Name == "Informatik")
                            {
                                item.EingebrachteHalbjahre = 4;
                                await db.UpdateAsync(item);
                            }
                        }
                        break;
                    case 1:
                        foreach (var item in Faecher)
                        {
                            if (item.Name == "Physik")
                            {
                                item.EingebrachteHalbjahre = 4;
                                await db.UpdateAsync(item);
                            }
                        }
                        break;
                    case 2:
                        foreach (var item in Faecher)
                        {
                            if (item.Name == "Chemie")
                            {
                                item.EingebrachteHalbjahre = 4;
                                await db.UpdateAsync(item);
                            }
                        }
                        break;
                    case 3:
                        foreach (var item in Faecher)
                        {
                            if (item.Name == "Biologie")
                            {
                                item.EingebrachteHalbjahre = 4;
                                await db.UpdateAsync(item);
                            }
                        }
                        break;
                }
                switch (fach1)
                {
                    case 0:
                        foreach (var item in Faecher)
                        {
                            if (item.Name == "Informatik")
                            {
                                item.EingebrachteHalbjahre = 4;
                                await db.UpdateAsync(item);
                            }
                        }
                        break;
                    case 1:
                        foreach (var item in Faecher)
                        {
                            if (item.Name == "Physik")
                            {
                                item.EingebrachteHalbjahre = 4;
                                await db.UpdateAsync(item);
                            }
                        }
                        break;
                    case 2:
                        foreach (var item in Faecher)
                        {
                            if (item.Name == "Chemie")
                            {
                                item.EingebrachteHalbjahre = 4;
                                await db.UpdateAsync(item);
                            }
                        }
                        break;
                    case 3:
                        foreach (var item in Faecher)
                        {
                            if (item.Name == "Biologie")
                            {
                                item.EingebrachteHalbjahre = 4;
                                await db.UpdateAsync(item);
                            }
                        }
                        break;
                }
            }
        }
    }
}
