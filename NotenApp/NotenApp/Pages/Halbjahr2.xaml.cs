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
        HalbjahrViewModel _model;
        public List<float?> durchschnitte = new List<float?>();
        public float? count= 0;
        public float? gesamtDurchschnitt;
        public Halbjahr2()
        {

            InitializeComponent();
            _model = BindingContext as HalbjahrViewModel;
            
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _model.Refresh();
            foreach (FachModel fach in _model.FaecherHJ2)
            {
                durchschnitte.Add(fach.Durchschnitt);
            }
            for (int i = 0; i < durchschnitte.Count; i++)
            {
                count += durchschnitte[i];
            }
            gesamtDurchschnitt = count / durchschnitte.Count;
            h.Text = gesamtDurchschnitt.ToString();
            count = 0;
        }

        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var fach = e.CurrentSelection.FirstOrDefault() as FachModel;
            await Navigation.PushAsync(new EntscheidungsSeite(fach));
        }



        private async void SwipeItem_Invoked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FachNotenSeite());
        }
    }
}