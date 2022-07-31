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

            fach.LkDurchschnitt = await GetLkDurchschnitt(fach);
            fach.GesamtDurchschnitt = GetFachDurchschnitt(fach);
            Console.WriteLine(fach.GesamtDurchschnitt);
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
        public static async Task<float?> GetLkDurchschnitt(FachModel fach)
        {
            float? durchschnittLk;
            float? countLk = 0;
            List<NotenModel> gesamtNoten = await db.Table<NotenModel>().ToListAsync();
            foreach (var item in gesamtNoten)
            {
                if(item.Type == 1 && item.Fach == fach.Name && item.Halbjahr == fach.Halbjahr)
                {
                    fach.LKNoten.Add(item.Note);
                }
            }
            for (int i = 0; i < fach.LKNoten.Count; i++)
            {
                if (fach.LKNoten[i] != null)
                {
                    countLk += (float?)fach.LKNoten[i];
                }
            }
            durchschnittLk = countLk / fach.LKNoten.Count;
            return durchschnittLk;
        }
        public static float? GetKlausurDurchschnitt(FachModel fach)
        {

            return null;
            
        }
        public static float? GetFachDurchschnitt(FachModel fach)
        {
            if(fach.LkDurchschnitt != null && fach.KlausurDurchschnitt != null)
            {
                float? x = (float?)fach.LkDurchschnitt + (float?)fach.KlausurDurchschnitt;
                double y= (double)x / 2;
                return (float?)Math.Round(y, 2);
            }
            else if(fach.LkDurchschnitt == null)
            {
                return fach.KlausurDurchschnitt;
            }
            else
            {
                double j = (double)fach.LkDurchschnitt;
                return (float?)Math.Round(j,2);
            }

        }
        public static async Task<float> GetHJGesamtDurchschnitt(int halbjahr)
        {
            var Gesamtfacher = await db.Table<FachModel>().ToListAsync();
            float gesamtDurchschnitt;
            float count = 0;
            float count2 = 0;
                            
            foreach (var item in Gesamtfacher)
            {
                if(item.Halbjahr == halbjahr && item.GesamtDurchschnitt != null)
                {
                    count += (float)item.GesamtDurchschnitt;
                    count2++;
                }
                
            }
            gesamtDurchschnitt = count / count2;
            return (float)Math.Round(gesamtDurchschnitt,2);
                    
            
        }

    }
}
