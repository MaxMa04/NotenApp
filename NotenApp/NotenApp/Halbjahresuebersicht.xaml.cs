using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotenApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Halbjahresuebersicht : TabbedPage
    {
        public Halbjahresuebersicht()
        {
            InitializeComponent();
        }
    }
}