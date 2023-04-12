using NotenApp.Services;
using NotenApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotenApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AbiturInfoPopup : Popup
	{
		public AbiturInfoPopup()
		{
			InitializeComponent();
			if(HalbjahrViewModel.Instance.FaecherHJ1.Count * 4 >= 40)
			{
				mehrFaecher.IsChecked = true;
			}
            if (HalbjahrViewModel.Instance.FaecherHJ1.Where(f => f.Durchschnitt != null).ToList().Count == HalbjahrViewModel.Instance.FaecherHJ1.Count && HalbjahrViewModel.Instance.FaecherHJ1.Count != 0)
            {
                notenHalbjahr1.IsChecked = true;
            }
            if (HalbjahrViewModel.Instance.FaecherHJ1.Where(f => f.IsLK).ToList().Count == 2)
			{
				leistunkskurs.IsChecked = true;
			}
			Task.Run(async () =>
			{
				int count = await FachService.CountPrFaecher();
				Device.BeginInvokeOnMainThread(() =>
				{
					if(count == 5)
					{
						prfaecher.IsChecked = true;
					}
				});
			
            });



		}

        private void btn_Clicked(object sender, EventArgs e)
        {
			Dismiss(null);
        }
    }
}