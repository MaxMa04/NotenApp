using NotenApp.Logic;
using NotenApp.Models;
using NotenApp.Services;
using NotenApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
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

        private async void AddOtherFremdsprache(object sender, EventArgs e) //für Fremdsprachen
        {
            string fachName = (string)await Navigation.ShowPopupAsync(new AddOtherFachPopup(true));
            if (fachName != null && fachName != String.Empty)
            {
                await Task.WhenAll(FachService.AddFach(fachName, (int)FachAufgabenfeld.Sprachlich, 1, 1, false, false, true),
                        FachService.AddFach(fachName, (int)FachAufgabenfeld.Sprachlich, 2, 1, false, false, true),
                        FachService.AddFach(fachName, (int)FachAufgabenfeld.Sprachlich, 3, 1, false, false, true),
                        FachService.AddFach(fachName, (int)FachAufgabenfeld.Sprachlich, 4, 1, false, false, true));
                var faecherToAdd = await FachService.GetFaecherWhenAdded(fachName);
                HalbjahrViewModel.Instance.FaecherHJ1.Add(faecherToAdd[0]);
                HalbjahrViewModel.Instance.FaecherHJ2.Add(faecherToAdd[1]);
                HalbjahrViewModel.Instance.FaecherHJ3.Add(faecherToAdd[2]);
                HalbjahrViewModel.Instance.FaecherHJ4.Add(faecherToAdd[3]);
            }
        }

        private async void AddOtherGK(object sender, EventArgs e) //für fächerverbindende Grundkurse
        { 
            string fachName = (string)await Navigation.ShowPopupAsync(new AddOtherFachPopup(false));
            if (fachName != null && fachName != String.Empty)
            {
                await Task.WhenAll(FachService.AddFach(fachName, (int)FachAufgabenfeld.Kein, 1, 1, false, false, false),
                        FachService.AddFach(fachName, (int)FachAufgabenfeld.Kein, 2, 1, false, false, false),
                        FachService.AddFach(fachName, (int)FachAufgabenfeld.Kein, 3, 1, false, false, false),
                        FachService.AddFach(fachName, (int)FachAufgabenfeld.Kein, 4, 1, false, false, false));
                var faecherToAdd = await FachService.GetFaecherWhenAdded(fachName);
                HalbjahrViewModel.Instance.FaecherHJ1.Add(faecherToAdd[0]);
                HalbjahrViewModel.Instance.FaecherHJ2.Add(faecherToAdd[1]);
                HalbjahrViewModel.Instance.FaecherHJ3.Add(faecherToAdd[2]);
                HalbjahrViewModel.Instance.FaecherHJ4.Add(faecherToAdd[3]);
            }
        }
    }                                  
}