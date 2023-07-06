using NotenApp.Logic;
using NotenApp.Services;
using NotenApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
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
			InitializeComponent();
            model= BindingContext as Block2ViewModel;
			
		}
        protected async override void OnAppearing()
        {
            base.OnAppearing(); 
            await model.InitBlock2();
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
            if (model.P1 == null || model.P1.Name == "-")
            {
                return;
            }
            int? note = (int?)await Navigation.ShowPopupAsync(new NotenPopup(WhichNote.Block2, NotenTyp.Schriftlich, model.P1.Name));
            await FachService.UpdateNote(note, 1, NotenTyp.Schriftlich);
            await model.InitBlock2();

        }
        private async void UpdateNoteSchriftlich2(object sender, EventArgs e)
        {
            if (model.P2 == null || model.P2.Name == "-")
            {
                return;
            }

            int? note = (int?)await Navigation.ShowPopupAsync(new NotenPopup(WhichNote.Block2, NotenTyp.Schriftlich, model.P2.Name));
            await FachService.UpdateNote(note, 2, NotenTyp.Schriftlich);
            await model.InitBlock2();
        }
        private async void UpdateNoteSchriftlich3(object sender, EventArgs e)
        {
            if (model.P3 == null || model.P3.Name == "-")
            {
                return;
            }
            int? note = (int?)await Navigation.ShowPopupAsync(new NotenPopup(WhichNote.Block2, NotenTyp.Schriftlich, model.P3.Name));
            await FachService.UpdateNote(note, 3, NotenTyp.Schriftlich);
            await model.InitBlock2();
        }
        //Mündliche Note
        private async void UpdateNachNoteMündlich1(object sender, EventArgs e)
        {

            if (model.P1 == null || model.P1.Name == "-" || model.P1.NoteSchriftlich is null)
            {
                return;
            }
            int? note = (int?)await Navigation.ShowPopupAsync(new NotenPopup(WhichNote.Block2, NotenTyp.Mündlich, model.P1.Name));
            await FachService.UpdateNote(note, 1, NotenTyp.NachNoteMündlich);
            await model.InitBlock2();
        }
        private async void UpdateNachNoteMündlich2(object sender, EventArgs e)
        {

            if (model.P2 == null || model.P2.Name == "-" || model.P2.NoteSchriftlich is null)
            {
                return;
            }
            int? note = (int?)await Navigation.ShowPopupAsync(new NotenPopup(WhichNote.Block2, NotenTyp.Mündlich, model.P2.Name));
            await FachService.UpdateNote(note, 2, NotenTyp.NachNoteMündlich);
            await model.InitBlock2();
        }
        private async void UpdateNachNoteMündlich3(object sender, EventArgs e)
        {

            if (model.P3 == null || model.P3.Name == "-" || model.P3.NoteSchriftlich is null)
            {
                return;
            }
            int? note = (int?)await Navigation.ShowPopupAsync(new NotenPopup(WhichNote.Block2, NotenTyp.Mündlich, model.P3.Name));
            await FachService.UpdateNote(note, 3, NotenTyp.NachNoteMündlich);
            await model.InitBlock2();
        }
        private  async void UpdateNachNoteMündlich4(object sender, EventArgs e)
        {

            if (model.P4 == null || model.P4.Name == "-"|| model.P4.NoteMündlich is null)
            {
                return;
            }
            int? note = (int?)await Navigation.ShowPopupAsync(new NotenPopup(WhichNote.Block2, NotenTyp.Mündlich, model.P4.Name));
            await FachService.UpdateNote(note, 4, NotenTyp.NachNoteMündlich);
            await model.InitBlock2();
        }
        private async void UpdateNachNoteMündlich5(object sender, EventArgs e)
        {

            if (model.P5 == null || model.P5.Name == "-" || model.P5.NoteMündlich is null)
            {
                return;
            }
            int? note = (int?)await Navigation.ShowPopupAsync(new NotenPopup(WhichNote.Block2, NotenTyp.Mündlich, model.P5.Name));
            await FachService.UpdateNote(note, 5, NotenTyp.NachNoteMündlich);
            await model.InitBlock2();
        }
        private async void UpdateNoteMündlich4(object sender, EventArgs e)
        {

            if (model.P4 == null || model.P4.Name == "-")
            {
                return;
            }
            int? note = (int?)await Navigation.ShowPopupAsync(new NotenPopup(WhichNote.Block2, NotenTyp.Mündlich, model.P4.Name));
            await FachService.UpdateNote(note, 4, NotenTyp.Mündlich);
            await model.InitBlock2();
        }
        private async void UpdateNoteMündlich5(object sender, EventArgs e)
        {

            if (model.P5 == null || model.P5.Name == "-")
            {
                return;
            }
            int? note = (int?)await Navigation.ShowPopupAsync(new NotenPopup(WhichNote.Block2, NotenTyp.Mündlich, model.P5.Name));
            await FachService.UpdateNote(note, 5, NotenTyp.Mündlich);
            await model.InitBlock2();
        }
    }
}