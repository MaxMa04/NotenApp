using MvvmHelpers;
using NotenApp.Logic;
using NotenApp.Models;
using NotenApp.Pages;
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
            if(db!= null)
            {
                return;
            }
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Dateeeen");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<HjFach>();
            await db.CreateTableAsync<PrFach>();
            await db.CreateTableAsync<HJNote>();
            await db.CreateTableAsync<Ziel>();
        }

        //
        //
        //
        //
        //
        //
        //Block 1
        //
        //
        //
        //
        //
        //
        //
        //
        public static async Task AddNote(HjFach fach, int note, NotenTyp notenTyp)
        {
            await Init();

            HJNote newNote = new HJNote
            {
                Note = note,
                Typ = (int)notenTyp,
                FachId = fach.Id
            };
            await db.InsertAsync(newNote);
            fach.Durchschnitt = await GetFachDurchschnitt(fach);
            await UpdateZielErforderlicheNoten(fach);
            await db.UpdateAsync(fach);
            if(fach.Name == "Informatik" || fach.Name == "Biologie" || fach.Name=="Physik"|| fach.Name == "Chemie")
            {
                await EntscheideBioInfoPhysikChemie();
            }
            if (fach.Name == "G/R/W" || fach.Name == "Geografie")
            {
                await EntscheideGeoGRW();
            }
            if (fach.IsFremdsprache == true)
            {
                await EntscheideFremdsprache();
            }


        }

        public static async Task<List<HjFach>> GetFaecher(int halbjahr)
        {
            await Init();
            var query = db.Table<HjFach>().Where(f => f.Halbjahr == halbjahr);
            List<HjFach> facher = await query.ToListAsync();
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

            HjFach fachh = await db.Table<HjFach>().Where(f => f.Id == fach.Id).FirstOrDefaultAsync();
            return fach.EingebrachteHalbjahre;
            
        }
        public static async Task<List<HjFach>> GetFaecher()
        {
            await Init();
            List<HjFach> Gesamtfacher = await db.Table<HjFach>().ToListAsync();
            return Gesamtfacher;
        }
        public static async Task<List<HJNote>> GetFachNoten(HjFach fach, NotenTyp notenTyp)
        {
            await Init();
            List<HJNote> Noten = await db.Table<HJNote>().Where(n => n.FachId == fach.Id && n.Typ == (int)notenTyp).ToListAsync();
            return Noten;
        }
        public static async Task<List<HJNote>> GetFachNotenHjView(HjFach fach, NotenTyp notenTyp)
        {
            await Init();
            List<HJNote> Noten = await db.Table<HJNote>().Where(n => n.FachId == fach.Id && n.Typ == (int)notenTyp).ToListAsync();
            List<HJNote> retNoten = new List<HJNote>(); 
            if(Noten.Count > 6)
            {
                for (int i = 0; i < 6; i++)
                {
                    retNoten.Add(Noten[i]);
                }
                return retNoten;
            }
            else
            {
                return Noten;
            }

            
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
        public static async Task UpdateFachState(HjFach fachh, string state)
        {
            await Init();
            List<HjFach> faecher = await db.Table<HjFach>().Where(f => f.Name == fachh.Name).ToListAsync();
            switch (state)
            {
                case "LK":
                    foreach (var fach in faecher)
                    {
                        fach.IsLK = true;
                        await db.UpdateAsync(fach);
                    }
                    await HalbjahrViewModel.Instance.Refresh(1);
                    await HalbjahrViewModel.Instance.Refresh(2);
                    await HalbjahrViewModel.Instance.Refresh(3);
                    await HalbjahrViewModel.Instance.Refresh(4);
                    break;
                case "GK":
                    foreach (var fach in faecher)
                    {
                        fach.IsLK = false;
                        await db.UpdateAsync(fach);
                    }
                    await HalbjahrViewModel.Instance.Refresh(1);
                    await HalbjahrViewModel.Instance.Refresh(2);
                    await HalbjahrViewModel.Instance.Refresh(3);
                    await HalbjahrViewModel.Instance.Refresh(4);
                    break;
                default:
                    break;
            }

        }
        public static async Task<int> GetLKCount()
        {
            await Init();
            int count = await db.Table<HjFach>().Where(f => f.IsLK).CountAsync();
            return count/4;            
        } 
        public static async Task RemoveSingleNote(HJNote note)
        {
            await Init();
            
            await db.DeleteAsync<HJNote>(note.Id);
            HjFach fach = await db.Table<HjFach>().Where(f => f.Id == note.FachId).FirstOrDefaultAsync();
            fach.Durchschnitt = await GetFachDurchschnitt(fach);
            await UpdateZielErforderlicheNoten(fach);
            await db.UpdateAsync(fach);
        }
        public static async Task RemoveFach(HjFach fach)
        {
            await Init();
            List<HjFach> list = await db.Table<HjFach>().Where(f => f.Name == fach.Name).ToListAsync();
            
            Ziel ziel = await db.Table<Ziel>().Where(z => z.FachId == fach.Id).FirstOrDefaultAsync();
            foreach (var item in list)
            {
                await db.DeleteAsync<HjFach>(item.Id);
                await RemoveAllNoten(item);
            }
            if (fach.IsPrFach)
            {
                PrFach prFach = await db.Table<PrFach>().Where(f => f.Name == fach.Name).FirstOrDefaultAsync();
                await UpdateName("-", prFach.PrNummer);
            }
            
            if(ziel != null)
            {
                await DeleteZiel(ziel);
            }
            
            
        }
        public static async Task RemoveAllNoten(HjFach fach)
        {
            await Init();
            List<HJNote> noten = await db.Table<HJNote>().Where(n => n.FachId == fach.Id).ToListAsync();
            foreach (var note in noten)
            {
                await db.DeleteAsync<HJNote>(note.Id); 
            }
        }
        public static async Task<float?> GetFachKlausurDurchschnitt(HjFach fach)
        {
            List<HJNote> klNoten = await db.Table<HJNote>().Where(n => n.Typ == (int)NotenTyp.Klausur && n.FachId == fach.Id).ToListAsync();
            float? sumKl = 0;
            float? duKL = 0;
            if(klNoten.Count == 0)
            {
                return null;
            }
            else
            {
                foreach (var note in klNoten)
                {
                    sumKl += note.Note;
                }
                duKL = sumKl / klNoten.Count;
                return duKL;
            }
        } 
        public static async Task<float?> GetFachLKDurchschnitt(HjFach fach)
        {
            List<HJNote> lkNoten = await db.Table<HJNote>().Where(n => n.Typ == (int)NotenTyp.LK && n.FachId == fach.Id).ToListAsync();
            float? sumLK = 0;
            float? duLK = 0;
            if (lkNoten.Count == 0)
            {
                return null;
            }
            else
            {
                foreach (var note in lkNoten)
                {
                    sumLK += note.Note;
                }
                duLK = sumLK / lkNoten.Count;
                return duLK;
            }
        }

        public static async Task<float?> GetFachDurchschnitt(HjFach fach)
        {
            float? durchschnittLk = await GetFachLKDurchschnitt(fach);
            
            float? durchschnittKlausur = await GetFachKlausurDurchschnitt(fach);

            
            if(durchschnittKlausur == null && durchschnittLk == null)
            {
                return null;
            }
            if(durchschnittKlausur == null)
            {
                return (float?)Math.Round((decimal)durchschnittLk,1);
            }
            else if(durchschnittLk == null)
            {
                return durchschnittKlausur;
            }
            else
            {
                float? durchschnitt = (durchschnittLk + durchschnittKlausur) / 2;
                return (float?)Math.Round((decimal)durchschnitt,1);
            }
        }
        public static async Task<float?> GetHJGesamtDurchschnitt(int halbjahr)
        {
            await Init();
            List<HjFach> faecher = await db.Table<HjFach>().Where(f => f.Durchschnitt != null && f.Halbjahr == halbjahr).ToListAsync();
            float gesamtDurchschnitt;
            float count = 0;
                            
            foreach (var fach in faecher)
            {
                count += (float)fach.Durchschnitt;
            }
            if(faecher.Count == 0)
            {
                return null;
            }
            else
            {
                gesamtDurchschnitt = count / faecher.Count;
                return (float)Math.Round(gesamtDurchschnitt, 1);
            }
        }
        public static async Task<HjFach> GetFach(HjFach fach)
        {
            await Init();
            HjFach fachd = await db.Table<HjFach>().Where(f => f.Id == fach.Id).FirstOrDefaultAsync();
            return fachd;   
        }
        //
        //
        //
        //
        //
        //
        //
        //Ziele
        //
        //
        //
        //
        //
        //
        //
        public static async Task AddZiel(HjFach fach, int? zielNote)
        {
            await Init();
            Ziel ziel = await GetFachZiel(fach);
            if (ziel == null && zielNote == null)
            {
                return;
            }
            else if(ziel == null && zielNote != null)
            {
                Ziel nziel = new Ziel
                {
                    ZielNote = zielNote,
                    FachId = fach.Id,
                    FachName = fach.Name,
                    Halbjahr = fach.Halbjahr
                };
                await db.InsertAsync(nziel);
                await UpdateZielErforderlicheNoten(fach);
            }
            else
            {
                
                if(ziel.FachId == fach.Id && zielNote != null)
                {
                    ziel.ZielNote = zielNote;
                    await db.UpdateAsync(ziel);
                    await UpdateZielErforderlicheNoten(fach);
                }
                else
                {
                    await DeleteZiel(ziel);
                }
                
            }

            

        }
        
        public static async Task DeleteZiel(Ziel ziel)
        {
            await Init();
            await db.DeleteAsync(ziel);
        }
        public static async Task UpdateZielErforderlicheNoten(HjFach fach)
        {
            await Init();
            Ziel ziel = await GetFachZiel(fach);
            if (ziel != null)
            {
                float zielNote = (float)ziel.ZielNote - 0.5f;
                List<HJNote> lks = await db.Table<HJNote>().Where(n => n.FachId == fach.Id && n.Typ == (int)NotenTyp.LK).ToListAsync();
                List<HJNote> klausuren = await db.Table<HJNote>().Where(n => n.FachId == fach.Id && n.Typ == (int)NotenTyp.Klausur).ToListAsync();
                if (klausuren.Count == 0 && lks.Count == 0)
                {
                    ziel.ErforderlicheKLNote = (int)ziel.ZielNote;
                    ziel.ErforderlicheLKNote = (int)ziel.ZielNote;
                    ziel.Span1 = " Punkte in ";
                    ziel.Span2 = " zu erreichen";
                    await db.UpdateAsync(ziel);
                    return;
                }
                float sumKl = 0;
                float sumLk = 0;
                foreach (var item in lks)
                {
                    sumLk += (float)item.Note;
                }
                foreach (var item in klausuren)
                {
                    sumKl += (float)item.Note;
                }
                float duKl = sumKl / klausuren.Count;
                float duLk = sumLk / lks.Count;
                if(lks.Count == 0 && klausuren.Count > 0)
                {
                    ziel.ErforderlicheLKNote = (int)Math.Round(zielNote * 2 - duKl);
                    ziel.ErforderlicheKLNote = (int)Math.Round(zielNote * (klausuren.Count + 1) - sumKl);
                    float ndukl = (sumKl + ziel.ErforderlicheKLNote) / (klausuren.Count + 1);
                    if (ndukl < zielNote)
                    {
                        ziel.ErforderlicheKLNote += 1;
                    }
                    if (ziel.ErforderlicheLKNote > 15 || ziel.ErforderlicheKLNote > 15)
                    {
                        ziel.Span1 = " Punkten in ";
                        ziel.Span2 = " näher zu kommen";
                        ziel.ErforderlicheKLNote = 15;
                        ziel.ErforderlicheLKNote = 15;
                    }
                    else if(ziel.ErforderlicheLKNote < 0 || ziel.ErforderlicheKLNote < 0)
                    {
                        ziel.Span1 = " Punkte in ";
                        ziel.Span2 = " zu erreichen";
                        ziel.ErforderlicheKLNote = 0;
                        ziel.ErforderlicheLKNote = 0;
                    }
                    else
                    {
                        ziel.Span1 = " Punkte in ";
                        ziel.Span2 = " zu erreichen";
                    }
                    
                    await db.UpdateAsync(ziel);
                }
                else if (lks.Count > 0 && klausuren.Count == 0)
                {
                    ziel.ErforderlicheLKNote = (int)Math.Round(zielNote * (lks.Count + 1) - sumLk);
                    ziel.ErforderlicheKLNote = (int)Math.Round(zielNote * 2 - duLk);
                    float nduLk = (sumLk + ziel.ErforderlicheLKNote) / (lks.Count + 1);
                    if(nduLk < zielNote)
                    {
                        ziel.ErforderlicheLKNote += 1;
                    }
                    if (ziel.ErforderlicheLKNote > 15 || ziel.ErforderlicheKLNote > 15)
                    {
                        ziel.Span1 = " Punkten in ";
                        ziel.Span2 = " näher zu kommen";
                        ziel.ErforderlicheKLNote = 15;
                        ziel.ErforderlicheLKNote = 15;
                    }
                    else if (ziel.ErforderlicheLKNote < 0 || ziel.ErforderlicheKLNote < 0)
                    {
                        ziel.Span1 = " Punkte in ";
                        ziel.Span2 = " zu erreichen";
                        ziel.ErforderlicheKLNote = 0;
                        ziel.ErforderlicheLKNote = 0;
                    }
                    else
                    {
                        ziel.Span1 = " Punkte in ";
                        ziel.Span2 = " zu erreichen";
                    }
                    
                    
                    await db.UpdateAsync(ziel);
                }
                else
                {
                    ziel.ErforderlicheLKNote = (int)Math.Round((zielNote * 2 - duKl) * (lks.Count + 1) - sumLk, 0);
                    ziel.ErforderlicheKLNote = (int)Math.Round((zielNote * 2 - duLk) * (klausuren.Count + 1) - sumKl, 0);
                    float ndukl = (sumKl + ziel.ErforderlicheKLNote) / (klausuren.Count + 1);
                    float nduLk = (sumLk + ziel.ErforderlicheLKNote) / (lks.Count + 1);
                    if((ndukl + duLk) / 2 < zielNote)
                    {
                        ziel.ErforderlicheKLNote += 1;
                    }
                    if((nduLk + duKl) / 2 < zielNote)
                    {
                        ziel.ErforderlicheLKNote += 1;
                    }
                    if(ziel.ErforderlicheLKNote > 15 || ziel.ErforderlicheKLNote > 15)
                    {
                        ziel.Span1 = " Punkten in ";
                        ziel.Span2 = " näher zu kommen";
                        ziel.ErforderlicheKLNote = 15;
                        ziel.ErforderlicheLKNote = 15;
                    }
                    else if (ziel.ErforderlicheLKNote < 0 || ziel.ErforderlicheKLNote < 0)
                    {
                        ziel.Span1 = " Punkte in ";
                        ziel.Span2 = " zu erreichen";
                        ziel.ErforderlicheKLNote = 0;
                        ziel.ErforderlicheLKNote = 0;
                    }
                    else
                    {
                        ziel.Span1 = " Punkte in ";
                        ziel.Span2 = " zu erreichen";
                    }
                    await db.UpdateAsync(ziel);
                }

            }
            else
            {
                return;
            }
        }
        public static async Task<Ziel> GetFachZiel(HjFach fach)
        {
            await Init();
            Ziel ziel = await db.Table<Ziel>().Where(z => z.FachId == fach.Id).FirstOrDefaultAsync();
            return ziel;
        }
        
        public static async Task<List<Ziel>> GetZiele()
        {
            await Init();
            var GesamtZiele = await db.Table<Ziel>().ToListAsync();
            return GesamtZiele;
        }
        //
        //
        //
        //
        //
        //Block2
        //
        //
        //
        //
        //
        //

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
        
        public static async Task UpdateName(string name, int prNummer)
        {
            await Init();
            PrFach prFach = await db.Table<PrFach>().Where(pf => pf.PrNummer == prNummer).FirstOrDefaultAsync();
            List<HjFach> HjFaecher = await db.Table<HjFach>().Where(hf => hf.Name == prFach.Name || hf.Name == name).ToListAsync();
            foreach (var hjFach in HjFaecher) //einzubringende Halbjahre werden angepasst für altes Fach
            { 
                if(hjFach.Name == prFach.Name)
                {
                    hjFach.EingebrachteHalbjahre = hjFach.MinHalbjahre;
                    hjFach.IsPrFach = false;
                    hjFach.IsLK = false;
                    await db.UpdateAsync(hjFach);
                }             
            }
            prFach.Name = name;
            prFach.NoteMündlich = null;
            prFach.NoteSchriftlich = null;
            prFach.Durchschnitt = null;
                    
            await db.UpdateAsync(prFach);
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
        public static async Task<int> GetPunktzahlBlock2()
        {
            await Init();
            float? durchschnitt = await GetDurchschnittBlock2();
            float? iPunktzahl =  durchschnitt * 20;
            if(iPunktzahl != null)
            { 

                return (int)Math.Round((decimal)iPunktzahl,0);
            }
            else
            {
                return 0;
            }

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
            if (abipunktzahl < 5)
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
            
            
            if(durchschnittBlock2 != null && punktzahlBlock1 > 4)
            {
                float punktzahlBlock2 = (float)Math.Round((double)durchschnittBlock2, 1) * 20;
                float abipunktzahl = punktzahlBlock2 + punktzahlBlock1;
                return abipunktzahl;
            }
            else if(durchschnittBlock2== null && punktzahlBlock1 > 4)
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
            List<HjFach> Faecher = new List<HjFach>();
            List<HjFach> faecher1 = await GetFaecher(1);
            List<HjFach> faecher2 = await GetFaecher();
            string nameFS1 = null;
            double sumFS1 = 0;
            double anzFS1 = 0;
            string nameFS2 = null;
            double sumFS2 = 0;
            double anzFS2 = 0;
            string nameFS3 = null;
            double sumFS3 = 0;
            double anzFS3 = 0;
            int anzprfaecher = 0;
            foreach (var item in faecher1)
            {
                if(nameFS1 == null && item.IsFremdsprache == true)
                {
                    nameFS1 = item.Name;
                }
                else if (nameFS2 == null && item.IsFremdsprache == true)
                {
                    nameFS2 = item.Name;
                }
                else if(nameFS3 == null && item.IsFremdsprache == true)
                {
                    nameFS3 = item.Name;
                }
            }
            foreach (var item in faecher2)
            {
                if(item.Name == nameFS1 || item.Name == nameFS2 || item.Name == nameFS3)
                {
                    Faecher.Add(item);
                }
            }
            foreach (var item in Faecher)
            {
                if(item.Name == nameFS1)
                {
                    if (item.Durchschnitt != null)
                    {
                        sumFS1 += (double)item.Durchschnitt;
                        anzFS1++;
                    }
                    if (item.IsPrFach == true)
                    {
                        sumFS1 = 0;
                        anzprfaecher++;
                    }
                    else
                    {
                        item.EingebrachteHalbjahre = item.MinHalbjahre;
                        await db.UpdateAsync(item);
                    }
                }
                else if (item.Name == nameFS2)
                {
                    if (item.Durchschnitt != null)
                    {
                        sumFS2 += (double)item.Durchschnitt;
                        anzFS2++;
                    }
                    if (item.IsPrFach == true)
                    {
                        sumFS2 = 0;
                        anzprfaecher++;
                    }
                    else
                    {
                        item.EingebrachteHalbjahre = item.MinHalbjahre;
                        await db.UpdateAsync(item);
                    }
                }
                else if (item.Name == nameFS3)
                {
                    if (item.Durchschnitt != null)
                    {
                        sumFS3 += (double)item.Durchschnitt;
                        anzFS3++;
                    }
                    if (item.IsPrFach == true)
                    {
                        sumFS3 = 0;
                        anzprfaecher++;
                    }
                    else
                    {
                        item.EingebrachteHalbjahre = item.MinHalbjahre;
                        await db.UpdateAsync(item);
                    }
                }

            }
            if (anzprfaecher > 0)
            {
                return;
            }
            else
            {
                double duFS1 = sumFS1 / anzFS1;
                double duFS2 = sumFS2 / anzFS2;
                double duFS3 = sumFS3 / anzFS3;
                int fach1 = 0;
                List<double> numbers = new List<double> { duFS1, duFS2, duFS3 };
                // Liste sortieren
                List<double> numbersa = numbers.ToList();
                numbers.Sort();
                for (int i = 0; i < numbers.Count; i++)
                {
                    if (numbers[2] == numbersa[i])
                    {
                        fach1 = i;
                    }
                }
                if (anzFS1 == 0 && anzFS2 == 0 && anzFS3 == 0)
                {
                    return;
                }
                switch (fach1)
                {
                    case 0:
                        foreach (var item in Faecher)
                        {
                            if (item.Name == nameFS1)
                            {
                                item.EingebrachteHalbjahre = 4;
                                await db.UpdateAsync(item);
                            }
                        }
                        break;
                    case 1:
                        foreach (var item in Faecher)
                        {
                            if (item.Name == nameFS2)
                            {
                                item.EingebrachteHalbjahre = 4;
                                await db.UpdateAsync(item);
                            }
                        }
                        break;
                    case 2:
                        foreach (var item in Faecher)
                        {
                            if (item.Name == nameFS3)
                            {
                                item.EingebrachteHalbjahre = 4;
                                await db.UpdateAsync(item);
                            }
                        }
                        break;
                    
                        
                    default:
                        break;
                }
            }
        }

        public static async Task EntscheideGeoGRW()
        {
            await Init();
            
            var query1 = db.Table<HjFach>().Where(f => f.Name == "Geografie"); 
            var query2 = db.Table<HjFach>().Where(f => f.Name == "G/R/W");
            List<HjFach> GeoFaecher = await query1.ToListAsync();
            List<HjFach> GRWFaecher = await query2.ToListAsync();
            if(GeoFaecher.Exists(f => f.IsPrFach == true) && GRWFaecher.Exists(f => f.IsPrFach))
            {
                return;
            }
            else if (GeoFaecher.Exists(f => f.IsPrFach == true))
            {
                foreach (var item in GRWFaecher)
                {
                    item.EingebrachteHalbjahre = item.MinHalbjahre;
                    await db.UpdateAsync(item);
                }
                return;
            }
            else if(GRWFaecher.Exists(f => f.IsPrFach == true))
            {
                foreach (var item in GeoFaecher)
                {
                    item.EingebrachteHalbjahre = item.MinHalbjahre;
                    await db.UpdateAsync(item);
                }
                return;
            }
            if (GeoFaecher.Any() != true)
            {
                foreach (var item in GRWFaecher)
                {
                    if(item.IsPrFach != true)
                    {
                        item.EingebrachteHalbjahre = 2;
                        await db.UpdateAsync(item);
                    }
                    else
                    {
                        item.EingebrachteHalbjahre = 4;
                        await db.UpdateAsync(item);
                    }
                }
                return;
            }
            if (GRWFaecher.Any() != true)
            {
                foreach (var item in GeoFaecher)
                {
                    if (item.IsPrFach != true)
                    {
                        item.EingebrachteHalbjahre = 2;
                        await db.UpdateAsync(item);
                    }
                    else
                    {
                        item.EingebrachteHalbjahre = 4;
                        await db.UpdateAsync(item);
                    }
                }
                return;
            }
            if(GeoFaecher.Any() && GRWFaecher.Any())
            {
                GeoFaecher = Controller.SortList(GeoFaecher);
                GRWFaecher = Controller.SortList(GRWFaecher);
                

                double? sumGeo = (double?)(GeoFaecher[0].Durchschnitt + GeoFaecher[1].Durchschnitt) / 2;
                
                double? sumGRW = (double?)(GRWFaecher[0].Durchschnitt + GRWFaecher[1].Durchschnitt) / 2; ;
                
                if (sumGeo > sumGRW)
                {
                    foreach (var item in GeoFaecher)
                    {
                        item.EingebrachteHalbjahre = 2;
                        await db.UpdateAsync(item);
                    }
                    foreach (var item in GRWFaecher)
                    {
                        item.EingebrachteHalbjahre = item.MinHalbjahre;
                        await db.UpdateAsync(item);
                    }
                }
                else
                {
                    foreach (var item in GeoFaecher)
                    {
                        item.EingebrachteHalbjahre = item.MinHalbjahre;
                        await db.UpdateAsync(item);
                    }
                    foreach (var item in GRWFaecher)
                    {
                        item.EingebrachteHalbjahre = 2;
                        await db.UpdateAsync(item);
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
                        if (item.Durchschnitt != null)
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
                            anzPh = 0;
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
            if (anzprfaecher >= 8)
            {
                return;
            }
            else if (anzprfaecher == 4)
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
                if (anzIn == 0 && anzPh == 0 && anzCh == 0 && anzBi == 0)
                {
                    return;
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
                    default:
                        break;
                }
            }
            else if (anzprfaecher == 0)
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
                if (anzIn == 0 && anzPh == 0 && anzCh == 0 && anzBi == 0)
                {
                    return;
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
            else
            {
                throw new Exception("Anzahl Prüfungsfächer falsch");
            }
        }
    }
}
