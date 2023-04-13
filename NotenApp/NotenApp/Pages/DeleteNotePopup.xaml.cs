using NotenApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotenApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DeleteNotePopup : Popup
	{
		public DeleteNotePopup ()
		{
			InitializeComponent ();
		}

        private async void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            await FachService.UpdateUserShowPopupWhenDeletingNote(false);
        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            Dismiss(true);
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            Dismiss(false);
        }
    }
}