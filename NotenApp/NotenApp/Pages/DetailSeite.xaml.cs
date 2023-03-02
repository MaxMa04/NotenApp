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
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotenApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailSeite : ContentPage
    {
        public HjFach fach;
        DetailViewModel model;
        public DetailSeite(HjFach fach)
        {
            InitializeComponent();
            this.fach = fach;
            model = BindingContext as DetailViewModel;
            model.FachName = fach.Name;
            Task.Run(async () => { await model.DetermineSwitchStartBehavior(fach, _switch); });
            Task.Run(async () => { await model.InitNoten(fach); });
            Task.Run(async () => { await model.InitEinzHj(fach); });
            Task.Run(async () => { await model.InitFachDurchschnitt(fach); });
            Task.Run(async () => { await model.InitZiel(fach); });
            

        }

        private async void DeleteNote(object sender, SelectionChangedEventArgs e)
        {
            var note = e.CurrentSelection.FirstOrDefault() as HJNote;
            await FachService.RemoveSingleNote(note);
            await model.InitNoten(fach);
            await model.InitEinzHj(fach);
            await model.InitFachDurchschnitt(fach);
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            int? note = (int?)await Navigation.ShowPopupAsync(new NotenPopup(WhichNote.Ziel));
            
            await FachService.AddZiel(fach, note);
            await model.InitZiel(fach);
          
        }

        private async void AddNote(object sender, EventArgs e)
        {
            NotenTyp? notenTyp = (NotenTyp?)await Navigation.ShowPopupAsync(new EntscheidungsPopup());
            int? note = null;

            if (notenTyp != null)
            {
                note = (int?)await Navigation.ShowPopupAsync(new NotenPopup(WhichNote.Block1));
            }
            
            if (notenTyp != null && note != null)
            {
                
                await FachService.AddNote(fach, (int)note, (NotenTyp)notenTyp);
                
                await model.InitNoten(fach);
                
            }

            await model.InitFachDurchschnitt(fach);
            await model.InitEinzHj(fach);
        }


        private async void CustomSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            await model.HandleSwitch(fach, _switch.IsToggled);
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
    
    }
}