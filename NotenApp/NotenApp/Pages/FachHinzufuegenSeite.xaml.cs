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


        private async void AddFaecher(object sender, EventArgs e)
        {
            foreach (var item in cv.SelectedItems)
            {
                var fach = item as HjFach;
                if (fach != null)
                {
                    await Task.WhenAll(FachService.AddFach(fach.Name, fach.Aufgabenfeld, 1, fach.MinHalbjahre, fach.IsLK, fach.IsPrFach, fach.IsFremdsprache),
                        FachService.AddFach(fach.Name, fach.Aufgabenfeld, 2, fach.MinHalbjahre, fach.IsLK, fach.IsPrFach, fach.IsFremdsprache),
                        FachService.AddFach(fach.Name, fach.Aufgabenfeld, 3, fach.MinHalbjahre, fach.IsLK, fach.IsPrFach, fach.IsFremdsprache),
                        FachService.AddFach(fach.Name, fach.Aufgabenfeld, 4, fach.MinHalbjahre, fach.IsLK, fach.IsPrFach, fach.IsFremdsprache));
                    var faecherToAdd = await FachService.GetFaecherWhenAdded(fach.Name);
                    HalbjahrViewModel.Instance.FaecherHJ1.Add(faecherToAdd[0]);
                    HalbjahrViewModel.Instance.FaecherHJ2.Add(faecherToAdd[1]);
                    HalbjahrViewModel.Instance.FaecherHJ3.Add(faecherToAdd[2]);
                    HalbjahrViewModel.Instance.FaecherHJ4.Add(faecherToAdd[3]);
                }
            }
            await Navigation.PopAsync();
        }
    }                                  
}