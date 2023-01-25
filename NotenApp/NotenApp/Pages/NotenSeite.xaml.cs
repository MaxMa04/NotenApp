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
    public partial class NotenSeite : Popup
    {
        private HjFach _fach2;
        NotenTyp notenTyp;
        HalbjahrViewModel hjVm;
        private int seitenZurück;
        bool isHalbjahr;
        bool isZiel;
        int prNummer;

        public NotenSeite(HjFach fach, NotenTyp notenTyp, int seitenZurück)
        {
            InitializeComponent();
            _fach2 = fach;
            this.notenTyp = notenTyp;
            hjVm = new HalbjahrViewModel();
            this.seitenZurück = seitenZurück;
            isHalbjahr = true;
            isZiel = false;
            btn.Text = "Zurück";
        }
        public NotenSeite(NotenTyp notenTyp, int prNummer)
        {
            InitializeComponent();
            this.notenTyp = notenTyp;  
            this.prNummer = prNummer;
            isHalbjahr = false;
            isZiel = false;
            btn.Text = "Keine Note";
        }
        public NotenSeite(HjFach fach)
        {
            InitializeComponent();
            _fach2 = fach;
            isHalbjahr = false;
            isZiel = true;
            btn.Text = "Ziel löschen";
        }
        private async void Button_Clicked1(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int note = Convert.ToInt32(button.Text);
            //für Block 1
            if(isHalbjahr == true)
            {
                
                Dismiss(note);
                
            }
            //für Prüfungen/Block2
            else if(isHalbjahr == false && isZiel == false)
            {
                await FachService.UpdateNote(note, prNummer, notenTyp);
                Dismiss(null);
            }
            // Für Ziele
            else
            {
                
                await FachService.AddZiel(_fach2.Halbjahr, _fach2.Name, note);
                Dismiss(null);
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if(isHalbjahr == true)
            {
                
                Dismiss(null);
            }
            else if (isHalbjahr == false && isZiel == false)
            {
                
                await FachService.UpdateNote(null, prNummer, notenTyp);
                Dismiss(null);
            }
            else
            {
                await FachService.AddZiel(_fach2.Halbjahr, _fach2.Name, null);
                Dismiss(null);
            }
        }
    }
}