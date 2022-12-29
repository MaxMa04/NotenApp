using NotenApp.Models;
using NotenApp.Services;
using NotenApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotenApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Block2Page : ContentPage
	{
        Block2ViewModel model;
		public Block2Page ()
		{
			InitializeComponent ();
            model= BindingContext as Block2ViewModel;
			
		}
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            model.P1 = await FachService.GetPrFach(1);
            model.P2 = await FachService.GetPrFach(2);
            model.P3 = await FachService.GetPrFach(3);
            model.P4 = await FachService.GetPrFach(4);
            model.P5 = await FachService.GetPrFach(5);
            model.DurchschnittBlock2 = await FachService.GetDurchschnittBlock2();
            model.PunktzahlBlock2 = await FachService.GetPunktzahlBlock2();
        }
        //FachName
        private async void SetPrFach1(object sender, EventArgs e)
        {
            if (model.P1 == null)
            {
                await Navigation.PushAsync(new PrFachWaehlenSeite(1,false));
            }
            else
            {
                await Navigation.PushAsync(new PrFachWaehlenSeite(1, true));
            }
            
        }
        private async void SetPrFach2(object sender, EventArgs e)
        {
            if (model.P2 == null)
            {
                await Navigation.PushAsync(new PrFachWaehlenSeite(2, false));
            }
            else
            {
                await Navigation.PushAsync(new PrFachWaehlenSeite(2, true));
            }
            
        }
        private async void SetPrFach3(object sender, EventArgs e)
        {
            if (model.P3 == null)
            {
                await Navigation.PushAsync(new PrFachWaehlenSeite(3, false));
            }
            else
            {
                await Navigation.PushAsync(new PrFachWaehlenSeite(3, true));
            }
            
        }
        private async void SetPrFach4(object sender, EventArgs e)
        {
            if (model.P4 == null)
            {
                await Navigation.PushAsync(new PrFachWaehlenSeite(4, false));
            }
            else
            {
                await Navigation.PushAsync(new PrFachWaehlenSeite(4, true));
            }
            
        }
        private async void SetPrFach5(object sender, EventArgs e)
        {
            if (model.P5 == null)
            {
                await Navigation.PushAsync(new PrFachWaehlenSeite(5, false));
            }
            else
            {
                await Navigation.PushAsync(new PrFachWaehlenSeite(5, true));
            }
            
        }
        // Schriftliche Note
        private async void UpdateNoteSchriftlich1(object sender, EventArgs e)
        {
            if (model.P1 == null)
            {
                return;
            }
            await Navigation.PushAsync(new NotenSeite(NotenTyp.Schriftlich, 1));
        }
        private async void UpdateNoteSchriftlich2(object sender, EventArgs e)
        {
            if (model.P2 == null)
            {
                return;
            }
            await Navigation.PushAsync(new NotenSeite(NotenTyp.Schriftlich, 2));
        }
        private async void UpdateNoteSchriftlich3(object sender, EventArgs e)
        {
            if (model.P3 == null)
            {
                return;
            }
            await Navigation.PushAsync(new NotenSeite(NotenTyp.Schriftlich, 3));
        }
        //Mündliche Note
        private async void UpdateNoteMündlich1(object sender, EventArgs e)
        {

            if (model.P1 == null)
            {
                return;
            }
            await Navigation.PushAsync(new NotenSeite(NotenTyp.Mündlich, 1));
        }
        private async void UpdateNoteMündlich2(object sender, EventArgs e)
        {

            if (model.P2 == null)
            {
                return;
            }
            await Navigation.PushAsync(new NotenSeite(NotenTyp.Mündlich, 2));
        }
        private async void UpdateNoteMündlich3(object sender, EventArgs e)
        {

            if (model.P3 == null)
            {
                return;
            }
            await Navigation.PushAsync(new NotenSeite(NotenTyp.Mündlich, 3));
        }
        private async void UpdateNoteMündlich4(object sender, EventArgs e)
        {

            if (model.P4 == null)
            {
                return;
            }
            await Navigation.PushAsync(new NotenSeite(NotenTyp.Mündlich, 4));
        }
        private async void UpdateNoteMündlich5(object sender, EventArgs e)
        {

            if (model.P5 == null)
            {
                return;
            }
            await Navigation.PushAsync(new NotenSeite(NotenTyp.Mündlich, 5));
        }
        private async void Delete(object sender, EventArgs e)
        {
            await FachService.Delete();
        }
    }
}