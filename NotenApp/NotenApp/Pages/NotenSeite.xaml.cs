using NotenApp.Logic;
using NotenApp.Models;
using NotenApp.Services;
using NotenApp.ViewModels;
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
    public partial class NotenSeite : ContentPage
    {
        private HjFach _fach2;
        NotenTyp notenTyp;
        HalbjahrViewModel viewModel2;
        private int seitenZurück;
        bool isHalbjahr;
        bool isZiel;
        int prNummer;

        public NotenSeite(HjFach fach, NotenTyp notenTyp, int seitenZurück)
        {
            InitializeComponent();
            _fach2 = fach;
            this.notenTyp = notenTyp;
            viewModel2 = new HalbjahrViewModel();
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
        }
        private async void Button_Clicked1(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int note = Convert.ToInt32(button.Text);
            if(isHalbjahr == true)
            {
                if (notenTyp == NotenTyp.LK)
                {
                    await viewModel2.AddNote(_fach2, note, NotenTyp.LK);
                    if (seitenZurück == 2)
                    {
                        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await Navigation.PopAsync();
                    }
                }
                else
                {
                    await viewModel2.AddNote(_fach2, note, NotenTyp.Klausur);

                    if (seitenZurück == 2)
                    {
                        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await Navigation.PopAsync();
                    }
                }
            }
            else if(isHalbjahr == false && isZiel == false)
            {
                await FachService.UpdateNote(note, prNummer, notenTyp);
                await Navigation.PopAsync();
            }
            else
            {
                await FachService.AddZiel(_fach2.Halbjahr, _fach2.Name, note);
                await Navigation.PopAsync();
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if(isHalbjahr == true)
            {
                
                await Navigation.PopAsync();
            }
            else
            {
                
                await FachService.UpdateNote(null, prNummer, notenTyp);
                await Navigation.PopAsync();
            }
        }
    }
}