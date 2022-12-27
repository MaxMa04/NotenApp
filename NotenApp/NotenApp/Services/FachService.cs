﻿using NotenApp.Models;
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
                Type = (int)notenTyp,
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
    }
}
