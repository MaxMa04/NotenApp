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

            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<FachModel>();
            await db.CreateTableAsync<NotenModel>();

        }

        public static async Task AddNote(FachModel fach, int note, int type)
        {
            await Init();

            NotenModel newNote = new NotenModel
            {
                Note = note,
                Type = type,
                Fach = fach.Name,
                Halbjahr = fach.Halbjahr
            };
            await db.InsertAsync(newNote);
            fach.Durchschnitt = await GetFachDurchschnitt(fach);
            await db.UpdateAsync(fach);


        }

        public static async Task<IEnumerable<FachModel>> GetFacher(int halbjahr)
        {
            await Init();
            var Gesamtfacher = await db.Table<FachModel>().ToListAsync();
            List<FachModel> facher = new List<FachModel>();
            foreach (var fach in Gesamtfacher)
            {
                if(fach.Halbjahr == halbjahr)
                {
                    facher.Add(fach);
                }
            }
            return facher;
        }
        public static async Task<IEnumerable<NotenModel>> GetNoten(int halbjahr)
        {
            await Init();
            var noten = await db.Table<NotenModel>().ToListAsync();
            List<NotenModel> Noten = new List<NotenModel>();
            foreach (var note in noten)
            {
                if (note.Halbjahr == halbjahr)
                {
                    Noten.Add(note);
                }
            }
            return Noten;
        }
        //nutzbar für alle
        public static async Task AddFach(string name, int halbjahr) 
        {
            await Init();

            FachModel fach = new FachModel
            {
                Name = name,
                Halbjahr = halbjahr
            };

            await db.InsertAsync(fach);
        }

        //muss drinnen bleiben
        public static async Task RemoveNote(int id)
        {
            await Init();
            
            await db.DeleteAsync<NotenModel>(id);
            return;
        }
        public static async Task RemoveFach(FachModel fach)
        {
            await Init();
            List<FachModel> list = await db.Table<FachModel>().ToListAsync();
            foreach (var item in list)
            {
                if(item.Name == fach.Name)
                {
                    await db.DeleteAsync<FachModel>(item.Id);
                }
            }
            await RemoveNote(fach);
            await db.DeleteAsync<FachModel>(fach.Id);
            return;
        }
        public static async Task RemoveNote(FachModel fach)
        {
            await Init();
            List<NotenModel> list = await db.Table<NotenModel>().ToListAsync();
            foreach (var item in list)
            {
                if (item.Fach == fach.Name)
                {
                    await db.DeleteAsync<NotenModel>(item.Id);
                }
            }
            return;
        }
        public static async Task<float?> GetFachDurchschnitt(FachModel fach)
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
            List<NotenModel> gesamtNoten = await db.Table<NotenModel>().ToListAsync();
            foreach (var item in gesamtNoten)
            {
                switch (item.Type)
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
                return 1;
            }
            if(hasKlausur == false)
            {
                return (float?)Math.Round((decimal)durchschnittLk,2);
            }
            else if(hasLk == false)
            {
                return durchschnittKlausur;
            }
            else
            {
                var y = durchschnittLk + durchschnittKlausur;
                durchschnitt = y / 2;
                return (float?)Math.Round((decimal)durchschnitt,2);
            }
            

        }
        public static async Task<float> GetHJGesamtDurchschnitt(int halbjahr)
        {
            await Init();
            var Gesamtfacher = await db.Table<FachModel>().ToListAsync();
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
            gesamtDurchschnitt = count / count2;
            return (float)Math.Round(gesamtDurchschnitt,2);
                    
            
        }

    }
}
