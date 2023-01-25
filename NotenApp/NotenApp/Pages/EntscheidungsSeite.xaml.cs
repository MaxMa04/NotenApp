using NotenApp.Logic;
using NotenApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotenApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntscheidungsSeite : Popup
    {
        private HjFach fach;

        public EntscheidungsSeite(HjFach fach)
        {
            InitializeComponent();
            this.fach = fach;
        }
        private void LK_Button_Clicked(object sender, EventArgs e)
        {
            
            //var result = await Navigation.ShowPopupAsync(new NotenSeite(fach, NotenTyp.LK, 2));
            NotenTyp s = NotenTyp.LK;
            Dismiss(s);
            //await Navigation.PushAsync(new NotenSeite(fach, NotenTyp.LK, 2));           
        }
        private void Klausur_Button_Clicked(object sender, EventArgs e)
        {
            Navigation.ShowPopup(new NotenSeite(fach, NotenTyp.Klausur, 2));
            //await Navigation.PushAsync(new NotenSeite(fach, NotenTyp.Klausur, 2));
        }
    }
}