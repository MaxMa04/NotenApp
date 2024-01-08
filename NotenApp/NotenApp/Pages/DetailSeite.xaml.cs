using MvvmHelpers;
using NotenApp.Logic;
using NotenApp.Models;
using NotenApp.Services;
using NotenApp.ViewModels;
using Switch;
using Switch.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotenApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailSeite : ContentPage
    {
        public HjFach fach;
        DetailViewModel model;
        bool wasLkbefore = false;
        public DetailSeite(HjFach fach)
        {
            InitializeComponent();
            this.fach = fach;
            model = BindingContext as DetailViewModel;
          
            //model.FachName = fach.Name;
            //Task[] task = new Task[] { model.InitNoten(fach), model.InitEinzHj(fach), model.InitFachDurchschnitt(fach), model.InitZiel(fach), model.InitEndnote(fach) };
            //Task.WhenAll(task);
            

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Task[] task = new Task[] { frame.TranslateTo(0, 0, 400, Easing.CubicOut), model.InitDetails(fach)};
            await Task.WhenAll(task);
            ScreenSizing();
            var user = await FachService.GetUserData();
            if(user.ShowDetailHelpPopup)
            {
                Navigation.ShowPopup(new DetailHelpPopup());
            }
            //if (fach.IsLK)
            //{
            //    wasLkbefore = true;
            //     _switch.IsToggled = true;
            //}
            //else
            //{
            //    int countLK = await FachService.GetLKCount();
            //    if (countLK < 2)
            //    {
            //        _switch.IsEnabled = true;
            //    }
            //    else
            //    {
            //        _switch.IsEnabled = false;
            //    }
            //}
            
        }
        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            //await HalbjahrViewModel.Instance.UpdateFachState(fach);
            
            
        }
        //private async void DeleteNote(object sender, SelectionChangedEventArgs e)
        //{
        //    
        //    var note = e.CurrentSelection.FirstOrDefault() as HJNote;
        //    if (note is null) return;
        //    var user = await FachService.GetUserData();
        //    bool delete = true;
        //    if (user.ShowPopupWhenDeletingNote)
        //    {
        //       delete = (bool)await Navigation.ShowPopupAsync(new DeleteNotePopup());
        //    }
        //    
        //    if (delete)
        //    {
        //        await FachService.RemoveSingleNote(note);
        //        model.Fach.LKNoten.Remove(note);
        //        //Task[] tasks = new Task[] { model.InitNoten(fach), model.InitEinzHj(fach), model.InitFachDurchschnitt(fach) };
        //        //await Task.WhenAll(tasks);
        //        await HalbjahrViewModel.Instance.ChangeHjDurchschnitt(fach.Halbjahr);
        //        await Task.WhenAll(FachService.UpdateUserB1(), UserViewModel.Instance.InitZiele());
        //    }
        //    else
        //    {
        //        lkn.SelectedItem = null;
        //        kln.SelectedItem = null;
        //        return;
        //
        //    }
        //
        //    
        //}

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            int? note = (int?)await Navigation.ShowPopupAsync(new NotenPopup(WhichNote.Ziel, NotenTyp.Ziel, fach.Name));
            if(note == -1)
            {
                var ziel = await FachService.GetFachZiel(fach);
                if (ziel == null) return;
                await FachService.DeleteZiel(ziel);
            }
            else if(note == null)
            {
                return;
            }
            else
            {
                await FachService.AddZiel(fach, note);
            }

            //await model.InitZiel(fach);
            await UserViewModel.Instance.InitZiele();   
          
        }

        //private async void AddNote(object sender, EventArgs e)
        //{
        //    NotenTyp? notenTyp = (NotenTyp?)await Navigation.ShowPopupAsync(new EntscheidungsPopup(fach.Name));
        //    int? note = null;
        //
        //    if (notenTyp != null)
        //    {
        //        note = (int?)await Navigation.ShowPopupAsync(new NotenPopup(WhichNote.Block1, (NotenTyp)notenTyp, fach.Name));
        //    }
        //    
        //    if (notenTyp != null && note != null)
        //    {
        //        
        //        //await FachService.AddNote(fach, (int)note, (NotenTyp)notenTyp);
        //        
        //    }
        //    else
        //    {
        //        return;
        //    }
        //    //Task[] tasks = new Task[] { model.InitNoten(fach), model.InitFachDurchschnitt(fach), model.InitEinzHj(fach)};
        //   // await Task.WhenAll(tasks);
        //    await HalbjahrViewModel.Instance.ChangeHjDurchschnitt(fach.Halbjahr);
        //    await Task.WhenAll(FachService.UpdateUserB1(), UserViewModel.Instance.InitZiele());
        //}


        private async void CustomSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if(wasLkbefore == true)
            {
                wasLkbefore = false;
                return;
            }
            await model.HandleSwitch(fach, _switch.IsToggled);
            await FachService.UpdateUserB1();
            
        }

        private void CustomSwitch_SwitchPanUpdate(object sender, Switch.Events.SwitchPanUpdatedEventArgs e)
        {
            Flex.TranslationX = -(e.TranslateX + e.XRef);
            string colorCode = string.Empty;
            switch (Settings.Theme)
            {
                case 0: //Rosa
                    colorCode = "#ffc0be";
                    break;
                case 1: //Blau
                    colorCode = "#B0E2FF";
                    
                    break;
                case 2: //Grün
                    colorCode = "#B3E6C9";
                    break;
            }

            Color fromColorLight = _switch.IsToggled ? Color.FromHex(colorCode) : Color.FromHex(colorCode);
            Color toColorLight = _switch.IsToggled ? Color.FromHex(colorCode) : Color.FromHex(colorCode);

            double t = e.Percentage * 0.01;

            _switch.KnobCornerRadius = _switch.IsToggled ? new CornerRadius(0, 5, 0, 5) : new CornerRadius(5, 0, 5, 0);
            _switch.KnobColor = ColorAnimationUtil.ColorAnimation(fromColorLight, toColorLight, t);
        }

        private void ScreenSizing()
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            if (mainDisplayInfo.Width / mainDisplayInfo.Density <= 375 && mainDisplayInfo.Height / mainDisplayInfo.Density <= 670)
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        grd.Padding = new Thickness(0, 0, 0, 40);
                        break;
                    case Device.Android:
                        grd.Padding = new Thickness(0, 0, 0, 35);
                        break;
                };
                rg1.Height = new GridLength(4, GridUnitType.Star);
                l1.FontSize = 50;
                rg21.Height = new GridLength(1.9, GridUnitType.Star);
                rg22.Height = new GridLength(1.9, GridUnitType.Star);
                rg23.Height = new GridLength(5.2, GridUnitType.Star);
                rg24.Height = new GridLength(1.7, GridUnitType.Star);
                f1.WidthRequest = 20;
            }
            else
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        grd.Padding = new Thickness(0, 0, 0, 70);
                        break;
                    case Device.Android:
                        grd.Padding = new Thickness(0, 0, 0, 35);
                        break;
                };

            }
            if ((mainDisplayInfo.Width / mainDisplayInfo.Density) >= 800 && (mainDisplayInfo.Height / mainDisplayInfo.Density >= 1050))
            {
                imgp.Scale = 1;
                f1.WidthRequest = 40;
                spa1.FontSize = 25;
                l2.FontSize = 25;
                l3.FontSize = 35;
                l5.FontSize = 35;
            }
        }
    }
}