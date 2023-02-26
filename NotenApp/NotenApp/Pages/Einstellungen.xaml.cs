using NotenApp.Services;
using NotenApp.Themes;
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
    public partial class Einstellungen : ContentPage
    {
        public Einstellungen()
        {
            InitializeComponent();
            switch (Settings.Theme)
            {
                case 0:
                    RadioButtonRosa.IsChecked = true;
                    break;
                case 1:
                    RadioButtonBlue.IsChecked = true;
                    break;
                case 2:
                    RadioButtonGreen.IsChecked = true;
                    break;
            }
        }
        void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

            string theme = (sender as RadioButton)?.Value as string;
            switch (theme)
            {
                case "Green":
                    Settings.SetTheme(2);
                    break;
                case "Blue":
                  
                    Settings.SetTheme(1);
                    break;
                case "Rosa":
                    Settings.SetTheme(0);
                    break;
            }
            

        }
    }
}