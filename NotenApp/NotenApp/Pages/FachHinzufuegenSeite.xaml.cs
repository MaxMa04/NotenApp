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

            await FachService.AddFach(fach.Name, 1, fach.MinHalbjahre);
            await FachService.AddFach(fach.Name, 2, fach.MinHalbjahre);
            await FachService.AddFach(fach.Name, 3, fach.MinHalbjahre);
            await FachService.AddFach(fach.Name, 4, fach.MinHalbjahre);
            
            await Navigation.PopAsync();
        }
    }
}