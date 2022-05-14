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
            if(db != null) return;
            
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<Fach>();

        }
        public static async Task AddNote(Fach fach, int note, int zahl)
        {
            await Init();
            await RemoveFach(fach.Id);
            if (fach.Note1 == null && zahl == 1)
            {
                fach.Note1 = note;
                await db.InsertAsync(fach);
            }
            else if(fach.Note2 == null && zahl == 1)
            {
                fach.Note2 = note;
                await db.InsertAsync(fach);
            }
            else if (fach.Note3 == null && zahl == 1)
            {
                fach.Note3 = note;
                await db.InsertAsync(fach);
            }
            else if (fach.Note4 == null && zahl == 1)
            {
                fach.Note4 = note;
                await db.InsertAsync(fach);
            }
            else if (fach.Note5 == null && zahl == 1)
            {
                fach.Note5 = note;
                await db.InsertAsync(fach);
            }
            else if (fach.Note6 == null && zahl == 1)
            {
                fach.Note6 = note;
                await db.InsertAsync(fach);
            }
            else if (fach.Note7 == null && zahl == 1)
            {
                fach.Note7 = note;
                await db.InsertAsync(fach);
            }
            else if (fach.KlausurNote1 == null && zahl == 2)
            {
                fach.KlausurNote1 = note;
                await db.InsertAsync(fach);
            }
            else if (fach.KlausurNote2 == null && zahl == 2)
            {
                fach.KlausurNote2 = note;
                await db.InsertAsync(fach);
            }
            else
            {
                await db.InsertAsync(fach);
            }
            
        }
        public static async Task AddFach(string name)
        {
            await Init();
            Fach fach = new Fach
            {
                Name = name
            };
            await db.InsertAsync(fach);
        }


        public static async Task RemoveFach(int id)
        {
            await Init();
            
            await db.DeleteAsync<Fach>(id);
            return;
        }

        public static async Task<IEnumerable<Fach>> GetFacher()
        {
            await Init();
            var facher = await db.Table<Fach>().ToListAsync();
            return facher;
        }
    }
}
