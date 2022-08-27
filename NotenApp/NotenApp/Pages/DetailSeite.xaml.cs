using MvvmHelpers;
using NotenApp.Models;
using NotenApp.Services;
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
    public partial class DetailSeite : ContentPage
    {
        public ObservableRangeCollection<NotenModel> LkNoten { get; set; }
        public ObservableRangeCollection<NotenModel> KlausurNoten { get; set; }
        public FachModel Fach { get; set; }
        public DetailSeite()
        {
            InitializeComponent();
            BindingContext = this;
            LkNoten = new ObservableRangeCollection<NotenModel>();
            KlausurNoten = new ObservableRangeCollection<NotenModel>();
        }
        public DetailSeite(FachModel fach)
        {
            this.Fach = fach;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            LkNoten = (ObservableRangeCollection<NotenModel>)await FachService.GetNoten(Fach.Halbjahr);
            KlausurNoten = (ObservableRangeCollection<NotenModel>)await FachService.GetNoten(Fach.Halbjahr);
        }
    }
}