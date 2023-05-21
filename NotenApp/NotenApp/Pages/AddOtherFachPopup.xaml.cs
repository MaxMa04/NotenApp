using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Effects.Semantic;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotenApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddOtherFachPopup : Popup
	{
		bool IsFremdsprache { get; set; }
		public AddOtherFachPopup (bool isFremdsprache)
		{
			InitializeComponent ();
			IsFremdsprache = isFremdsprache;
			if (IsFremdsprache)
			{
				heading.Text = "Neue Fremdsprache hinzufügen";
			
			}
			else
			{
				heading.Text = "Fächerverbindender Grundkurs";
			}
			
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
			Dismiss(fach.Text);
        }
    }
}