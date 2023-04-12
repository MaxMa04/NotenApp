using MvvmHelpers;
using NotenApp.Models;
using NotenApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace NotenApp.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private static UserViewModel instance = new UserViewModel();
        private float? gesamtDurchschnittHJ1;
        private float? gesamtDurchschnittHJ2;
        private float? gesamtDurchschnittHJ3;
        private float? gesamtDurchschnittHJ4;
        private float? durchschnittBlock2;
        private UserModel user;

        public static UserViewModel Instance { get { return instance; } }
        public UserModel User
        {
            get => user;
            set
            {
                user = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(User)));
            }
        }
        public ObservableRangeCollection<Ziel> Ziele { get; set; }
        public float? GesamtDurchschnittHJ1
        {
            get => gesamtDurchschnittHJ1;
            set
            {
                gesamtDurchschnittHJ1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GesamtDurchschnittHJ1)));
            }
        }
        public float? GesamtDurchschnittHJ2
        {
            get => gesamtDurchschnittHJ2;
            set
            {
                gesamtDurchschnittHJ2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GesamtDurchschnittHJ2)));
            }
        }
        public float? GesamtDurchschnittHJ3
        {
            get => gesamtDurchschnittHJ3;
            set
            {
                gesamtDurchschnittHJ3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GesamtDurchschnittHJ3)));
            }
        }
        public float? GesamtDurchschnittHJ4
        {
            get => gesamtDurchschnittHJ4;
            set
            {
                gesamtDurchschnittHJ4 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GesamtDurchschnittHJ4)));
            }
        }
        public float? DurchschnittBlock2
        {
            get => durchschnittBlock2;
            set
            {
                durchschnittBlock2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DurchschnittBlock2)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public UserViewModel()
        {
            Ziele = new ObservableRangeCollection<Ziel>();
        }
        public async Task InitUser()
        {
            await FachService.CreateUserIfNotExists();
            User = await FachService.GetUserData();
        }
        public async Task InitZiele()
        {
            Ziele.Clear();
            List<Ziel> ziele = await FachService.GetZiele();
            Ziele.AddRange(ziele);
        }
    }
}
