using MvvmHelpers;
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
    public partial class DetailSeite : ContentPage
    {
        public FachModel fach;
        DetailViewModel model;
        public DetailSeite(FachModel fach)
        {
            InitializeComponent();
            this.fach = fach;
            model = BindingContext as DetailViewModel;
            Label.Text = fach.Name;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await model.Initialize(fach);
            model.FachDurchschnitt = (float)await FachService.GetFachDurchschnitt(fach);
            
            

        }



    }
}