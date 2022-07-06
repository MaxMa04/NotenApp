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

        }

        public static async Task AddNote2(FachModel fach, int note, int zahl)
        {
            await Init();
            await RemoveFach(fach.Id);
            if (fach.Note1 == null && zahl == 1)
            {
                fach.Note1 = note;
                fach.Durchschnitt = GetDurchschnitt(fach, 1);
                await db.InsertAsync(fach);
                
            }
            else if (fach.Note2 == null && zahl == 1)
            {
                fach.Note2 = note;
                fach.Durchschnitt = GetDurchschnitt(fach, 2);
                await db.InsertAsync(fach);

            }
            else if (fach.Note3 == null && zahl == 1)
            {
                fach.Note3 = note;
                fach.Durchschnitt = GetDurchschnitt(fach,3);
                await db.InsertAsync(fach);
            }
            else if (fach.Note4 == null && zahl == 1)
            {
                fach.Note4 = note;
                fach.Durchschnitt = GetDurchschnitt(fach, 4);
                await db.InsertAsync(fach);
            }
            else if (fach.Note5 == null && zahl == 1)
            {
                fach.Note5 = note;
                fach.Durchschnitt = GetDurchschnitt(fach, 5);
                await db.InsertAsync(fach);
            }
            else if (fach.Note6 == null && zahl == 1)
            {
                fach.Note6 = note;
                fach.Durchschnitt = GetDurchschnitt(fach, 6);
                await db.InsertAsync(fach);
            }
            else if (fach.Note7 == null && zahl == 1)
            {
                fach.Note7 = note;
                fach.Durchschnitt = GetDurchschnitt(fach, 7);
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

        //mit Anpassungen nutzbar für alle (in der Form nicht)
        public static async Task RemoveFach(int id)
        {
            await Init();
            
            await db.DeleteAsync<FachModel>(id);
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
            await db.DeleteAsync<FachModel>(fach.Id);
            return;
        }
        public static float GetDurchschnitt(FachModel fach, int notenNr)
        {
            fach.LKNoten.Add(fach.Note1);
            fach.LKNoten.Add(fach.Note2);
            fach.LKNoten.Add(fach.Note3);
            fach.LKNoten.Add(fach.Note4);
            fach.LKNoten.Add(fach.Note5);
            fach.LKNoten.Add(fach.Note6);
            fach.LKNoten.Add(fach.Note7);
            fach.LKNoten.Add(fach.Note8);
            fach.LKNoten.Add(fach.Note9);
            fach.LKNoten.Add(fach.Note10);
            fach.LKNoten.Add(fach.Note11);
            fach.LKNoten.Add(fach.Note12);

            float durchschnitt;
            float count = 0;
            for (int i = 0; i < notenNr; i++)
            {
                if (fach.LKNoten[i] != null)
                {
                    count += (float)fach.LKNoten[i];
                }


            }
            durchschnitt = count / notenNr;



            return durchschnitt;
            

        }



    }
}
