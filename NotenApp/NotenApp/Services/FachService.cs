using NotenApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace NotenApp.Services
{
    public static class FachService
    {

        static SQLiteAsyncConnection db;
        static async Task Init()
        {
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
        public static async Task AddFach(string name, int halbjahr) 
        {
            await Init();

            HjFach fach = new HjFach
            {
                Name = name,
                Halbjahr = halbjahr
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
            foreach (var item in PrFaecher)
            {
                if(item.PrNummer == prNummer)
                {
                    item.Name = name;
                    await db.UpdateAsync(item);
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
    }
}
