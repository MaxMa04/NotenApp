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
    public partial class Halbjahr2 : ContentPage
    {
        Halbjahr2ViewModel _model;
        public Halbjahr2()
        {

            InitializeComponent();
            _model = BindingContext as Halbjahr2ViewModel;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _model.Refresh();
            foreach(var fach in _model.FaecherHJ2)
            {
                fach.LKNoten.Add(fach.Note1);
                fach.LKNoten.Add(fach.Note2);
                fach.LKNoten.Add(fach.Note3);
                fach.LKNoten.Add(fach.Note4);
                fach.LKNoten.Add(fach.Note5);
                fach.LKNoten.Add(fach.Note6);
                fach.LKNoten.Add(fach.Note7);
                fach.LKNoten.Add(fach.Note8);
                fach.LKNoten.Add(fach.Note9);
                fach.LKNoten.Add(fach.Note10);
                fach.LKNoten.Add(fach.Note11);
                fach.LKNoten.Add(fach.Note12);
            }
        }

        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var fach = e.CurrentSelection.FirstOrDefault() as Halbjahr2Model;
            await Navigation.PushAsync(new EntscheidungsSeite(fach));
        }



        private async void SwipeItem_Invoked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FachNotenSeite());
        }
    }
}