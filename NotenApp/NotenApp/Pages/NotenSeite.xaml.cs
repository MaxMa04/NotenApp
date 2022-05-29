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
        private Halbjahr1Model _fach;
        private Halbjahr2Model _fach2;
        private int _entscheidung;
        private int hjNummer;
        Halbjahr1ViewModel viewModel1;
        Halbjahr2ViewModel viewModel2;

        public NotenSeite(Halbjahr1Model fach, int entscheidung)
        {
            InitializeComponent();
            _fach = fach;
            _entscheidung = entscheidung;
            hjNummer = 1;
            viewModel1 = new Halbjahr1ViewModel();   
        }
        public NotenSeite(Halbjahr2Model fach, int entscheidung)
        {
            InitializeComponent();
            _fach2 = fach;
            _entscheidung = entscheidung;
            viewModel2 = new Halbjahr2ViewModel();
            hjNummer = 2;
        }

        private async void Button_Clicked1(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int note = Convert.ToInt32(button.Text);
            switch (hjNummer)
            {
                case 1:
                    if (_entscheidung == 1)
                    {
                        await viewModel1.AddNote(_fach, note, 1);

                        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await viewModel1.AddNote(_fach, note, 2);

                        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

                        await Navigation.PopAsync();
                    }
                    break;

                case 2:
                    if (_entscheidung == 1)
                    {
                        await viewModel2.AddNote(_fach2, note, 1);

                        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await viewModel2.AddNote(_fach2, note, 2);

                        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

                        await Navigation.PopAsync();
                    }
                    break;
            }
            
        }
    }
}