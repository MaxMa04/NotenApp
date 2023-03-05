using NotenApp.Models;
using NotenApp.Services;
using NotenApp.ViewModels;
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
        FachHinzufuegenViewModel vm;
        public FachHinzufuegenSeite()
        {
            InitializeComponent();
            vm = BindingContext as FachHinzufuegenViewModel;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await vm.InitHjFaecher();
        }



        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var fach = e.CurrentSelection.FirstOrDefault() as HjFach;

            await FachService.AddFach(fach.Name, fach.Aufgabenfeld, 1, fach.MinHalbjahre, fach.IsLK, fach.IsPrFach, fach.IsFremdsprache);
            await FachService.AddFach(fach.Name, fach.Aufgabenfeld, 2, fach.MinHalbjahre, fach.IsLK, fach.IsPrFach, fach.IsFremdsprache);
            await FachService.AddFach(fach.Name, fach.Aufgabenfeld, 3, fach.MinHalbjahre, fach.IsLK, fach.IsPrFach, fach.IsFremdsprache);
            await FachService.AddFach(fach.Name, fach.Aufgabenfeld, 4, fach.MinHalbjahre, fach.IsLK, fach.IsPrFach, fach.IsFremdsprache);
            
            
            Task[] tasks = new Task[] { HalbjahrViewModel.Instance.Refresh(1), HalbjahrViewModel.Instance.Refresh(2), HalbjahrViewModel.Instance.Refresh(3), HalbjahrViewModel.Instance.Refresh(4) };
            await Task.WhenAll(tasks);
            await Navigation.PopAsync();
        }                              
    }                                  
}