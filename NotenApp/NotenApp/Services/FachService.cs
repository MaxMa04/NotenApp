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

            await db.CreateTableAsync<Halbjahr1Model>();
            await db.CreateTableAsync<Halbjahr2Model>();

        }
        public static async Task AddNote(Halbjahr1Model fach, int note, int zahl)
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
        public static async Task<IEnumerable<Halbjahr1Model>> GetFaecherHJ1()
        {
            await Init();
            var facher = await db.Table<Halbjahr1Model>().ToListAsync();
            return facher;
        }
        public static async Task<IEnumerable<Halbjahr2Model>> GetFaecherHJ2()
        {
            await Init();
            var facher = await db.Table<Halbjahr2Model>().ToListAsync();
            return facher;
        }
        //nutzbar für alle
        public static async Task AddFach(string name) 
        {
            await Init();
            Halbjahr1Model fach = new Halbjahr1Model
            {
                Name = name
            };
            Halbjahr2Model fach2 = new Halbjahr2Model
            {
                Name = name
            };
            await db.InsertAsync(fach);
            await db.InsertAsync(fach2);
        }

        //mit Anpassungen nutzbar für alle (in der Form nicht)
        public static async Task RemoveFach(int id)
        {
            await Init();
            
            await db.DeleteAsync<Halbjahr1Model>(id);
            await db.DeleteAsync<Halbjahr2Model>(id);
            return;
        }


    }
}
