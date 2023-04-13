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
	public partial class ErweiterteEinstellungen : ContentPage
	{
		public ErweiterteEinstellungen ()
		{
			InitializeComponent ();
			
		}
        protected override async void OnAppearing()
        {
            base.OnAppearing();
			var user = await FachService.GetUserData();
			if (user.ShowPopupWhenDeletingNote)
			{
				_switch.IsToggled = true;
			}
        }
        private async void Switch_Toggled(object sender, ToggledEventArgs e)
        {
			await FachService.UpdateUserShowPopupWhenDeletingNote(true);
        }
    }
}