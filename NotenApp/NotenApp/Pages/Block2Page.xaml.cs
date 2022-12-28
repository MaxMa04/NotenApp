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
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
			model.P1 = new PrFach()
			{
                Name = "Spanisch"
			};
			
        }
    }
}