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
				_switch1.IsToggled = true;
			}
            if (user.ShowFachHelpPopup)
            {
                _switch2.IsToggled = true;
            }
            if (user.ShowDetailHelpPopup)
            {
                _switch3.IsToggled = true;
            }
        }
        private async void Switch1_Toggled(object sender, ToggledEventArgs e)
        {
			await FachService.UpdateUserShowPopupWhenDeletingNote(_switch1.IsToggled);
        }
        private async void Switch2_Toggled(object sender, ToggledEventArgs e)
        {
            await FachService.UpdateUserShowFachHelpPopup(_switch2.IsToggled);
        }
        private async void Switch3_Toggled(object sender, ToggledEventArgs e)
        {
            await FachService.UpdateUserShowDetailHelpPopup(_switch3.IsToggled);
        }
    }
}