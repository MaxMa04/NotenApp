using NotenApp.Models;
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
        private int _entscheidung;
        HalbjahrViewModel viewModel2;
        private int seitenZurück;

        public NotenSeite(HjFach fach, int entscheidung, int seitenZurück)
        {
            InitializeComponent();
            _fach2 = fach;
            _entscheidung = entscheidung;
            viewModel2 = new HalbjahrViewModel();
            this.seitenZurück = seitenZurück;
        }

        private async void Button_Clicked1(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int note = Convert.ToInt32(button.Text);
            if (_entscheidung == 1)
            {
                await viewModel2.AddNote(_fach2, note, NotenTyp.LK);
                if(seitenZurück == 2)
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
    }
}