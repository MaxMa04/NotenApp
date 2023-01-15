using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotenApp.Cells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FachCell : Grid
    {
        public FachCell()
        {
            InitializeComponent();
        }
    }
}