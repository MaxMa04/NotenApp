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
        public static async Task AddNote(Fach fach, int note)
        {
            await Init();
            await RemoveFach(fach.Id);
            if (fach.Note1 == null)
            {
                fach.Note1 = note;
                await db.InsertAsync(fach);
            }
            else if(fach.Note2 == null)
            {
                fach.Note2 = note;
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
