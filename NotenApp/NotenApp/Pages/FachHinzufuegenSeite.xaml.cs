using NotenApp.Models;
using NotenApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotenApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FachHinzufuegenSeite : ContentPage
    {
        public FachHinzufuegenSeite()
        {
            InitializeComponent();
        }

        private async void myListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            var fach = e.SelectedItem as HjFach;

            await FachService.AddFach(fach.Name, fach.Aufgabenfeld, 1, fach.MinHalbjahre, fach.IsLK, fach.IsPrFach);
            await FachService.AddFach(fach.Name, fach.Aufgabenfeld, 2, fach.MinHalbjahre, fach.IsLK, fach.IsPrFach);
            await FachService.AddFach(fach.Name, fach.Aufgabenfeld, 3, fach.MinHalbjahre, fach.IsLK, fach.IsPrFach);
            await FachService.AddFach(fach.Name, fach.Aufgabenfeld, 4, fach.MinHalbjahre, fach.IsLK, fach.IsPrFach);
            
            await Navigation.PopAsync();
        }
    }
}