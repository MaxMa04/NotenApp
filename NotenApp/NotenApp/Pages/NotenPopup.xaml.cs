using NotenApp.Logic;
using NotenApp.Models;
using NotenApp.Services;
using NotenApp.ViewModels;
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
    public partial class NotenPopup : Popup
    {
        

        public NotenPopup(WhichNote note)
        {
            InitializeComponent();
            switch (note)
            {
                case WhichNote.Block1:
                    btn.Text = "Zurück";
                    break;
                case WhichNote.Block2:
                    btn.Text = "Keine Note";
                    break;
                case WhichNote.Ziel:
                    btn.Text = "Kein Ziel";
                    break;
            }
            
        }
        private void Button_Clicked1(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int note = Convert.ToInt32(button.Text);
            Dismiss(note);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Dismiss(null);
        }
    }
}