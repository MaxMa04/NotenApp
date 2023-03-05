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
    public partial class PrFachWaehlenSeite : ContentPage
    {
        int prNummer;
        bool created;
        
        public PrFachWaehlenSeite(int prNummer, bool isCreated)
        {
            InitializeComponent();
            this.prNummer = prNummer;
            created = isCreated;
            BindingContext = HalbjahrViewModel.Instance;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await HalbjahrViewModel.Instance.Refresh(1);
        }
        private async void  CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var fach = e.CurrentSelection.FirstOrDefault() as HjFach;
            if (created == true)
            {
                await FachService.UpdateName(fach.Name, prNummer);
                
                await Navigation.PopAsync();
                await HalbjahrViewModel.Instance.Refresh(1);
                await HalbjahrViewModel.Instance.Refresh(2);
                await HalbjahrViewModel.Instance.Refresh(3);
                await HalbjahrViewModel.Instance.Refresh(4);
            }
            else
            {
                await FachService.AddPrFach(fach.Name, prNummer);
                
                await Navigation.PopAsync();
                await HalbjahrViewModel.Instance.Refresh(1);
                await HalbjahrViewModel.Instance.Refresh(2);
                await HalbjahrViewModel.Instance.Refresh(3);
                await HalbjahrViewModel.Instance.Refresh(4);
            }
            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (created == true)
            {
                await FachService.UpdateName("-", prNummer);
                await Navigation.PopAsync();
                await HalbjahrViewModel.Instance.Refresh(1);
                await HalbjahrViewModel.Instance.Refresh(2);
                await HalbjahrViewModel.Instance.Refresh(3);
                await HalbjahrViewModel.Instance.Refresh(4);

            }
            else
            {
                await Navigation.PopAsync();
            }
        }
    }
}